using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HudayiPortal.Application.Features.Mesajlar.Queries.GetMesajlar;
using HudayiPortal.Application.Interfaces;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChatController : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUserService _currentUserService;
	private readonly IMediator _mediator;

	public ChatController(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMediator mediator)
	{
		_unitOfWork = unitOfWork;
		_currentUserService = currentUserService;
		_mediator = mediator;
	}

	[HttpGet("groups")]
	[ProducesResponseType(typeof(List<ChatListDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetChatGroups(CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId;

		// 1. DAHİL OLUNAN GRUPLAR
		var userGroups = await _unitOfWork.Repository<ChatGrupUyesi>()
			.Where(u => u.KullaniciId == userId && u.SilindiMi != true)
			.Include(u => u.ChatGrup)
				.ThenInclude(g => g.Mesajlar)
			.ToListAsync(cancellationToken);

		var groupChatList = new List<ChatListDto>();
		foreach (var membership in userGroups)
		{
			var group = membership.ChatGrup;
			var lastMsg = group.Mesajlar
				.Where(m => m.SilindiMi != true)
				.OrderByDescending(m => m.OlusturulmaTarihi)
				.FirstOrDefault();

			var unreadCount = group.Mesajlar
				.Count(m => m.SilindiMi != true && m.GonderenId != userId && m.OkunduMu != true);

			groupChatList.Add(new ChatListDto(
				Id: group.Id,
				Name: group.GrupAdi,
				Avatar: group.GrupResmiUrl,
				IsGroup: true,
				LastMessage: lastMsg?.MesajIcerigi,
				LastMessageDate: lastMsg?.OlusturulmaTarihi,
				UnreadCount: unreadCount
			));
		}

		// 2. DM BİREBİR SOHBETLER
		// Kullanıcının attığı veya aldığı tüm silinmemiş mesajları çekelim
		var allDirectMessages = await _unitOfWork.Repository<Mesaj>()
			.Where(m => m.SilindiMi != true && m.ChatGrupId == null && (m.GonderenId == userId || m.AliciId == userId))
			.Include(m => m.Gonderen)
			.Include(m => m.Alici)
			.ToListAsync(cancellationToken);

		// Eşleşen diğer kullanıcıların ID'lerini gruplayalım
		var dmPartners = allDirectMessages
			.Select(m => m.GonderenId == userId ? m.Alici : m.Gonderen)
			.Where(p => p != null)
			.GroupBy(p => p!.Id)
			.Select(g => g.First())
			.ToList();

		var dmChatList = new List<ChatListDto>();
		foreach (var partner in dmPartners)
		{
			var partnerId = partner!.Id;
			var chatMessages = allDirectMessages
				.Where(m => (m.GonderenId == userId && m.AliciId == partnerId) || (m.GonderenId == partnerId && m.AliciId == userId))
				.OrderByDescending(m => m.OlusturulmaTarihi)
				.ToList();

			var lastMsg = chatMessages.FirstOrDefault();
			var unreadCount = chatMessages.Count(m => m.GonderenId == partnerId && m.OkunduMu != true);

			dmChatList.Add(new ChatListDto(
				Id: partnerId,
				Name: $"{partner.Ad} {partner.Soyad}",
				Avatar: partner.ProfilResmiUrl,
				IsGroup: false,
				LastMessage: lastMsg?.MesajIcerigi,
				LastMessageDate: lastMsg?.OlusturulmaTarihi,
				UnreadCount: unreadCount
			));
		}

		// İki listeyi birleştirip en son mesaj tarihine göre azalan şekilde sıralayalım
		var unifiedList = groupChatList.Concat(dmChatList)
			.OrderByDescending(c => c.LastMessageDate ?? DateTime.MinValue)
			.ToList();

		return Ok(unifiedList);
	}

	[HttpGet("messages/{groupIdOrUserId}")]
	[ProducesResponseType(typeof(List<MesajDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetChatMessages(
		int groupIdOrUserId,
		[FromQuery] bool isGroup,
		CancellationToken cancellationToken)
	{
		var query = new GetMesajlarQuery(
			AliciId: isGroup ? null : groupIdOrUserId,
			ChatGrupId: isGroup ? groupIdOrUserId : null
		);

		var messages = await _mediator.Send(query, cancellationToken);
		return Ok(messages);
	}

	[HttpPost("groups")]
	[ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
	public async Task<IActionResult> CreateChatGroup([FromBody] CreateGroupRequest request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId;

		var newGroup = new ChatGrubu
		{
			GrupAdi = request.GrupAdi,
			GrupResmiUrl = request.GrupResmiUrl,
			OlusturanKullaniciId = userId,
			OlusturulmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};

		await _unitOfWork.Repository<ChatGrubu>().AddAsync(newGroup, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		// Kurucuyu gruba ekleyelim (Admin olarak)
		var creatorMember = new ChatGrupUyesi
		{
			ChatGrupId = newGroup.Id,
			KullaniciId = userId,
			IsAdmin = true,
			KatilmaTarihi = DateTime.UtcNow,
			SilindiMi = false
		};
		await _unitOfWork.Repository<ChatGrupUyesi>().AddAsync(creatorMember, cancellationToken);

		// Diğer seçilen üyeleri ekleyelim
		if (request.KullaniciIds != null)
		{
			foreach (var memberId in request.KullaniciIds.Where(id => id != userId))
			{
				var member = new ChatGrupUyesi
				{
					ChatGrupId = newGroup.Id,
					KullaniciId = memberId,
					IsAdmin = false,
					KatilmaTarihi = DateTime.UtcNow,
					SilindiMi = false
				};
				await _unitOfWork.Repository<ChatGrupUyesi>().AddAsync(member, cancellationToken);
			}
		}

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return StatusCode(StatusCodes.Status201Created, newGroup.Id);
	}

	[HttpPost("messages/mark-read")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	public async Task<IActionResult> MarkMessagesAsRead([FromBody] MarkReadRequest request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId;

		var query = _unitOfWork.Repository<Mesaj>()
			.Where(m => m.SilindiMi != true && m.OkunduMu != true);

		if (request.ChatGrupId.HasValue)
		{
			// Grup sohbetinde gelen mesajları okundu olarak işaretleyelim
			query = query.Where(m => m.ChatGrupId == request.ChatGrupId.Value && m.GonderenId != userId);
		}
		else if (request.AliciId.HasValue)
		{
			// DM sohbetinde o kullanıcıdan gelen mesajları okundu olarak işaretleyelim
			query = query.Where(m => m.ChatGrupId == null && m.GonderenId == request.AliciId.Value && m.AliciId == userId);
		}
		else
		{
			return BadRequest("Sohbet grubu kimliği veya alıcı kimliği verilmelidir.");
		}

		var unreadMessages = await query.ToListAsync(cancellationToken);
		foreach (var msg in unreadMessages)
		{
			msg.OkunduMu = true;
		}

		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return NoContent();
	}

	[HttpGet("users")]
	[ProducesResponseType(typeof(List<ChatUserDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetChatUsers(CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId;

		var users = await _unitOfWork.Repository<Kullanici>()
			.Where(k => k.SilindiMi != true && k.Id != userId)
			.Include(k => k.Rol)
			.Select(k => new ChatUserDto(
				k.Id,
				k.Ad,
				k.Soyad,
				k.Email ?? string.Empty,
				k.Rol.RolAdi
			))
			.ToListAsync(cancellationToken);

		return Ok(users);
	}

	[HttpGet("groups/{groupId}/members")]
	[ProducesResponseType(typeof(List<GroupMemberDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetGroupMembers(int groupId, CancellationToken cancellationToken)
	{
		var members = await _unitOfWork.Repository<ChatGrupUyesi>()
			.Where(m => m.ChatGrupId == groupId && m.SilindiMi != true)
			.Include(m => m.Kullanici)
				.ThenInclude(k => k.Rol)
			.Select(m => new GroupMemberDto(
				m.KullaniciId,
				$"{m.Kullanici.Ad} {m.Kullanici.Soyad}",
				m.Kullanici.ProfilResmiUrl,
				m.Kullanici.Rol.RolAdi,
				m.IsAdmin ?? false
			))
			.ToListAsync(cancellationToken);

		return Ok(members);
	}
}

public record CreateGroupRequest(string GrupAdi, string? GrupResmiUrl, List<int> KullaniciIds);
public record MarkReadRequest(int? ChatGrupId, int? AliciId);
public record ChatListDto(
	int Id,
	string Name,
	string? Avatar,
	bool IsGroup,
	string? LastMessage,
	DateTime? LastMessageDate,
	int UnreadCount
);
public record ChatUserDto(
	int Id,
	string Ad,
	string Soyad,
	string Email,
	string RolAdi
);
public record GroupMemberDto(
	int KullaniciId,
	string AdSoyad,
	string? ProfilResmiUrl,
	string RolAdi,
	bool IsAdmin
);
