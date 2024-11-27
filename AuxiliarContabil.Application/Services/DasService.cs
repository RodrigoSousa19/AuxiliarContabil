using AutoMapper;
using AuxiliarContabil.API.Assertions;
using AuxiliarContabil.Domain.Dto;
using AuxiliarContabil.Domain.Interfaces.Repositories;
using AuxiliarContabil.Domain.Interfaces.Services;
using AuxiliarContabil.Infrastructure;

namespace AuxiliarContabil.Application.Services;

public class DasService : IDasService
{
    private readonly IRepository<DAS> _repository;
    private readonly IMapper _mapper;
    
    public DasService(IRepository<DAS> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<DasDto>> GetAllAsync()
    {
        var das = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<DasDto>>(das);
    }

    public async Task<DasDto?> GetByIdAsync(int id)
    {
        var das = await _repository.GetByIdAsync(id);
        return _mapper.Map<DasDto>(das);
    }

    public async Task AddAsync(DasDto dasDto)
    {
        Assertions.ClearErrors();
        
        Assertions.IsGreaterThanZero(dasDto.ReceitaBrutaAnual, nameof(dasDto.ReceitaBrutaAnual));
        
        Assertions.Validate();
        
        var das = _mapper.Map<DAS>(dasDto);
        await _repository.AddAsync(das);
    }

    public async Task UpdateAsync(DasDto dasDto)
    {
        var das = _mapper.Map<DAS>(dasDto);
        await _repository.UpdateAsync(das);
    }

    public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
}