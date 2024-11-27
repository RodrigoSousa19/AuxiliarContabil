using AutoMapper;
using AuxiliarContabil.API.Assertions;
using AuxiliarContabil.Domain.Dto;
using AuxiliarContabil.Domain.Interfaces.Repositories;
using AuxiliarContabil.Domain.Interfaces.Services;
using AuxiliarContabil.Infrastructure;

namespace AuxiliarContabil.Application.Services;

public class ComposicaoSalarialService : IComposicaoSalarialService
{
    private readonly IComposicaoSalarialRepository _repository;
    private readonly IMapper _mapper;

    public ComposicaoSalarialService(IComposicaoSalarialRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ComposicaoSalarioDto>> GetAllAsync()
    {
        var composicoes = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ComposicaoSalarioDto>>(composicoes);
    }

    public async Task<ComposicaoSalarioDto?> GetByIdAsync(int id)
    {
        var composicao = await _repository.GetByIdAsync(id);
        return composicao == null ? null : _mapper.Map<ComposicaoSalarioDto>(composicao);
    }

    public async Task AddAsync(ComposicaoSalarioDto composicaoDto)
    {
        Assertions.ClearErrors();
        
        Assertions.IsValidDate(composicaoDto.InicioPeriodo,nameof(composicaoDto.InicioPeriodo));
        Assertions.IsValidDate(composicaoDto.FimPeriodo,nameof(composicaoDto.FimPeriodo));
        
        Assertions.IsGreaterThanZero(composicaoDto.SalarioHora,nameof(composicaoDto.SalarioHora));
        
        Assertions.Validate();
        
        var composicao = _mapper.Map<ComposicaoSalario>(composicaoDto);
        await _repository.AddAsync(composicao);
    }

    public async Task UpdateAsync(ComposicaoSalarioDto composicaoDto)
    {
        var composicao = _mapper.Map<ComposicaoSalario>(composicaoDto);
        await _repository.UpdateAsync(composicao);
    }

    public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

    public async Task UpdateCurrentComposition(int id) => await _repository.UpdateCurrentComposition(id);
}