using AutoMapper;
using AuxiliarContabil.Domain.Dto;
using AuxiliarContabil.Domain.Entities;

namespace AuxiliarContabil.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ComposicaoSalario, ComposicaoSalarioDto>().ReverseMap();
        CreateMap<DAS, DasDto>().ReverseMap();
        CreateMap<ExtratoBancarioPessoaJuridica, ExtratoBancarioPessoaJuridicaDTO>().ReverseMap();
        CreateMap<Feriado,FeriadosDto>().ReverseMap();
    }
}