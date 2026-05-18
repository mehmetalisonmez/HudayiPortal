namespace HudayiPortal.Application.Features.Odalar.Queries.GetOdaYerlesim;

public sealed record OdaYerlesimResultDto(
	List<OdaDetailDto> Odalar,
	List<OdasizOgrenciDto> OdasizOgrenciler
);
