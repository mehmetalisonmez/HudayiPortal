using System;
using System.Linq;
using System.Threading.Tasks;
using HudayiPortal.Application.Features.Mesajlar.Commands.CreateMesaj;
using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Infrastructure.Hubs;

[Authorize]
public class ChatHub : Hub
{
	private readonly IMediator _mediator;
	private readonly IUnitOfWork _unitOfWork;

	public ChatHub(IMediator mediator, IUnitOfWork unitOfWork)
	{
		_mediator = mediator;
		_unitOfWork = unitOfWork;
	}

	public override async Task OnConnectedAsync()
	{
		var userIdString = Context.UserIdentifier;
		if (int.TryParse(userIdString, out int userId))
		{
			var userGroups = await _unitOfWork.Repository<ChatGrupUyesi>()
				.Where(u => u.KullaniciId == userId && u.SilindiMi != true)
				.Select(u => u.ChatGrupId)
				.ToListAsync();

			foreach (var groupId in userGroups)
			{
				await Groups.AddToGroupAsync(Context.ConnectionId, groupId.ToString());
			}
		}
		await base.OnConnectedAsync();
	}

	public async Task SendMessageAsync(CreateMesajCommand command)
	{
		await _mediator.Send(command);

		if (command.ChatGrupId.HasValue)
		{
			await Clients.Group(command.ChatGrupId.Value.ToString())
				.SendAsync("ReceiveMessage", command);
			return;
		}

		if (command.AliciId.HasValue)
		{
			await Clients.User(command.AliciId.Value.ToString())
				.SendAsync("ReceiveMessage", command);
		}
	}
}
