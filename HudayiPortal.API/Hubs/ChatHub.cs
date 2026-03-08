using HudayiPortal.Application.Features.Mesajlar.Commands.SendMessage;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace HudayiPortal.API.Hubs;

public class ChatHub : Hub
{
	private readonly IMediator _mediator;

	public ChatHub(IMediator mediator)
	{
		_mediator = mediator;
	}

	public async Task SendMessage(SendMessageCommand command)
	{
		await _mediator.Send(command);
		await Clients.All.SendAsync("ReceiveMessage", command);
	}
}
