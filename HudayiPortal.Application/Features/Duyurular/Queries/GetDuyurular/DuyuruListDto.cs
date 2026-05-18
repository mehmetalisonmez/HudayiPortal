namespace HudayiPortal.Application.Features.Duyurular.Queries.GetDuyurular;

public sealed record DuyuruListDto(
    int Id,
    string Baslik,
    string Icerik,
    DateTime? YayinTarihi,
    DateTime? OlusturulmaTarihi,
    int? HedefRolId,
    string? HedefRolAdi
);
