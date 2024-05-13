using AutoMapper;
using OnboardingApi.Domain.Dtos;
using OnboardingApi.Domain.Enums;
using OnboardingApi.Domain.Models;

namespace OnboardingApi.Mapping
{
	public class DtoToModelProfile : Profile
	{
		public DtoToModelProfile()
		{
			CreateMap<TotverDto, Totver>();

      CreateMap<AtividadeDto, Atividade>();

      CreateMap<OnboardingDto, Onboarding>()
        .ForMember(src => src.StatusOnboarding, opt => opt.MapFrom(src => (StatusEnum)src.StatusOnboarding));

      CreateMap<AtividadeOnboardingDto, AtividadeOnboarding>()
				.ForMember(src => src.StatusAtividade, opt => opt.MapFrom(src => (StatusEnum)src.StatusAtividade));
			
			
		}
	}
}