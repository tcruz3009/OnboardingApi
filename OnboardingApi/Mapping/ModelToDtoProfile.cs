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

      CreateMap<Atividade, AtividadeDto>();

      CreateMap<Onboarding, OnboardingDto>();

      CreateMap<AtividadeOnboarding, AtividadeOnboardingDto>();
    }
  }
}