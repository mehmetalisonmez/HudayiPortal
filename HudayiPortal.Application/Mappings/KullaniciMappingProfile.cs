using AutoMapper;
using HudayiPortal.Application.Features.Kullanicilar.Commands.CreateKullanici;
using HudayiPortal.Domain.Entities;

namespace HudayiPortal.Application.Mappings;

public sealed class KullaniciMappingProfile : Profile
{
	public KullaniciMappingProfile()
	{
		CreateMap<CreateKullaniciCommand, Kullanici>()
			.ForMember(dest => dest.SifreHash, opt => opt.Ignore())
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.ProfilResmiUrl, opt => opt.Ignore())
			.ForMember(dest => dest.EmailDogrulandiMi, opt => opt.Ignore())
			.ForMember(dest => dest.AktifMi, opt => opt.Ignore())
			.ForMember(dest => dest.OlusturulmaTarihi, opt => opt.Ignore())
			.ForMember(dest => dest.GuncellenmeTarihi, opt => opt.Ignore())
			.ForMember(dest => dest.SilindiMi, opt => opt.Ignore());
	}
}