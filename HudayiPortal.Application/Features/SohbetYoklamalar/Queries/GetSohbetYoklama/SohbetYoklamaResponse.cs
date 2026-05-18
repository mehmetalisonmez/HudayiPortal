namespace HudayiPortal.Application.Features.SohbetYoklamalar.Queries.GetSohbetYoklama;

public sealed record SohbetYoklamaResponse(
	int SohbetId,
	List<SohbetOgrenciDurumDto> Ogrenciler
);
