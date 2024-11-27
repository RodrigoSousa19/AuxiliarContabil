using AutoMapper;
using AuxiliarContabil.API.Assertions;
using AuxiliarContabil.Domain.Dto;
using AuxiliarContabil.Domain.Interfaces.Repositories;
using AuxiliarContabil.Domain.Interfaces.Services;
using AuxiliarContabil.Infrastructure;

namespace AuxiliarContabil.Application.Services;

public class ExtratoBancarioService : IExtratoBancarioService
{
    private readonly IRepository<ExtratoBancarioPessoaJuridica> _repository;
    private readonly IMapper _mapper;
    private const int seed = 50;
    
    public ExtratoBancarioService(IRepository<ExtratoBancarioPessoaJuridica> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ExtratoBancarioPessoaJuridicaDTO>> GetAllAsync()
    {
        var extratos = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ExtratoBancarioPessoaJuridicaDTO>>(extratos);
    }

    public async Task<ExtratoBancarioPessoaJuridicaDTO?> GetByIdAsync(int id)
    {
        var extrato = await _repository.GetByIdAsync(id);
        return _mapper.Map<ExtratoBancarioPessoaJuridicaDTO>(extrato);
    }

    public async Task AddAsync(ExtratoBancarioPessoaJuridicaDTO extratoDto)
    {
        var extrato = _mapper.Map<ExtratoBancarioPessoaJuridica>(extratoDto);
        await _repository.AddAsync(extrato);
    }

    public async Task AddAsync(IEnumerable<ExtratoBancarioPessoaJuridicaDTO> extratos)
    {
        var loteExtratos = extratos.GroupBy(x => (x.NomeBanco, x.TipoTransacao)).ToList();

        for (int lote = 0; lote < loteExtratos.Count; lote++)
        {
            for (int itens = 0; itens < loteExtratos[lote].Count(); itens += seed)
            {
                var itensDto = loteExtratos[lote].Skip(itens).Take(seed).ToList();
                var itensToInsert = _mapper.Map<List<ExtratoBancarioPessoaJuridica>>(itensDto);
                await _repository.AddRangeAsync(itensToInsert);
            }
        }
    }
    
    public async Task UpdateAsync(ExtratoBancarioPessoaJuridicaDTO extratoDto)
    {
        var extrato = _mapper.Map<ExtratoBancarioPessoaJuridica>(extratoDto);
        await _repository.UpdateAsync(extrato);
    }

    public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
}