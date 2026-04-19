using MediatR;

namespace HudayiPortal.Application.Features.Kullanicilar.Commands.DeleteKullanici;

public sealed record DeleteKullaniciCommand(int Id) : IRequest<Unit>;
