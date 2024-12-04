﻿using AutoMapper;
using AuxiliarContabil.Domain.Dto;
using AuxiliarContabil.Domain.Interfaces.Repositories;
using AuxiliarContabil.Domain.Interfaces.Services;
using AuxiliarContabil.Infrastructure;

namespace AuxiliarContabil.Application.Services;

public class FeriadosService : IFeriadosService
{
    private readonly IRepository<Feriado> _repository;
    private readonly IMapper _mapper;

    public FeriadosService(IRepository<Feriado> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FeriadosDto>> GetAllAsync()
    {
        var feriados = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<FeriadosDto>>(feriados);
    }
}