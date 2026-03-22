using HudayiPortal.Application.Features.Mesajlar.Commands.CreateMesaj;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace HudayiPortal.Infrastructure.Hubs;

[Authorize]
public class ChatHub : Hub
{
	private readonly IMediator _mediator;

	public ChatHub(IMediator mediator)
	{
		_mediator = mediator;
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
