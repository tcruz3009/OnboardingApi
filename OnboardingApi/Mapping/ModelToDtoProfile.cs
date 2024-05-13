using AutoMapper;
using OnboardingApi.Domain.Dtos;
using OnboardingApi.Domain.Models;
using OnboardingApi.Domain.Models.Queries;
using OnboardingApi.Extensions;

namespace OnboardingApi.Mapping
{
  public class ModelToDtoProfile : Profile
  {
    public ModelToDtoProfile()
    {
      CreateMap<Totver, TotverDto>();

      CreateMap<QueryResult<Atividade>, QueryResultList<AtividadeDto>>();

      CreateMap<Onboarding, OnboardingDto>()
                .ForMember(src => src.StatusOnboarding,
                           opt => opt.MapFrom(src => src.StatusOnboarding.ToDescriptionString()));

      CreateMap<AtividadeOnboarding, AtividadeOnboardingDto>()
                .ForMember(src => src.StatusAtividade,
                           opt => opt.MapFrom(src => src.StatusAtividade.ToDescriptionString()));
    }
  }
}