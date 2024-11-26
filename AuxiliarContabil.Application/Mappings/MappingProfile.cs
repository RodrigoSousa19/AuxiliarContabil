using AutoMapper;
using AuxiliarContabil.Domain.Dto;
using AuxiliarContabil.Infrastructure;

namespace AuxiliarContabil.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ComposicaoSalario, ComposicaoSalarioDto>().ReverseMap();
    }
}