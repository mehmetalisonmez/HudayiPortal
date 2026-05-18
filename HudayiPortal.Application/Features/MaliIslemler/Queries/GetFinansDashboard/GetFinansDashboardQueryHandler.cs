using HudayiPortal.Domain.Entities;
using HudayiPortal.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HudayiPortal.Application.Features.MaliIslemler.Queries.GetFinansDashboard;

public sealed class GetFinansDashboardQueryHandler
	: IRequestHandler<GetFinansDashboardQuery, FinansDashboardDto>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetFinansDashboardQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<FinansDashboardDto> Handle(
		GetFinansDashboardQuery request,
		CancellationToken cancellationToken)
	{
		// ─── Veri çek ────────────────────────────────────
		var query = _unitOfWork.Repository<MaliIslem>()
			.Where(m => m.SilindiMi != true)
			.Include(m => m.Yon)
			.Include(m => m.Kategori)
			.AsQueryable();

		if (request.BaslangicTarihi.HasValue)
			query = query.Where(m => m.IslemTarihi >= request.BaslangicTarihi.Value);

		if (request.BitisTarihi.HasValue)
			query = query.Where(m => m.IslemTarihi <= request.BitisTarihi.Value);

		var islemler = await query
			.Select(m => new
			{
				m.YonId,
				m.Tutar,
				m.IslemTarihi,
				m.KategoriId,
				KategoriAdi = m.Kategori != null ? m.Kategori.KategoriAdi : "Diğer"
			})
			.ToListAsync(cancellationToken);

		// ─── Toplam Gelir / Gider ─────────────────────────
		// YonId=1 → Gelir, YonId=2 → Gider
		var toplamGelir = islemler.Where(m => m.YonId == 1).Sum(m => m.Tutar);
		var toplamGider = islemler.Where(m => m.YonId == 2).Sum(m => m.Tutar);
		var netKasa = toplamGelir - toplamGider;

		// ─── Kategori Dağılımı (Giderler için) ──────────
		var giderler = islemler.Where(m => m.YonId == 2).ToList();
		var kategoriDagilimi = giderler
			.GroupBy(m => m.KategoriAdi)
			.Select(g => new
			{
				KategoriAdi = g.Key,
				Tutar = g.Sum(m => m.Tutar)
			})
			.OrderByDescending(g => g.Tutar)
			.Select(g => new KategoriDagilimiDto(
				g.KategoriAdi,
				g.Tutar,
				toplamGider > 0 ? Math.Round((double)(g.Tutar / toplamGider) * 100, 1) : 0))
			.ToList();

		// ─── Aylık Trend (son 12 ay) ──────────────────────
		var bugun = DateTime.UtcNow;
		var aylikTrend = new List<AylikTrendDto>();

		for (int i = 11; i >= 0; i--)
		{
			var hedefAy = new DateTime(bugun.Year, bugun.Month, 1).AddMonths(-i);
			var ayLabel = hedefAy.ToString("yyyy-MM");

			var gelir = islemler
				.Where(m => m.YonId == 1
					&& m.IslemTarihi.Year == hedefAy.Year
					&& m.IslemTarihi.Month == hedefAy.Month)
				.Sum(m => m.Tutar);

			var gider = islemler
				.Where(m => m.YonId == 2
					&& m.IslemTarihi.Year == hedefAy.Year
					&& m.IslemTarihi.Month == hedefAy.Month)
				.Sum(m => m.Tutar);

			aylikTrend.Add(new AylikTrendDto(ayLabel, gelir, gider));
		}

		return new FinansDashboardDto(toplamGelir, toplamGider, netKasa, kategoriDagilimi, aylikTrend);
	}
}
