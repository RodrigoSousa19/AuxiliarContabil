using AuxiliarContabil.Domain.Dto;

namespace AuxiliarContabil.Domain.Interfaces.Services;

public interface IExtratoBancarioService
{
    Task<IEnumerable<ExtratoBancarioPessoaJuridicaDTO>> GetAllAsync();
    Task<ExtratoBancarioPessoaJuridicaDTO?> GetByIdAsync(int id);
    Task AddAsync(ExtratoBancarioPessoaJuridicaDTO extratoDto);
    Task AddAsync(IEnumerable<ExtratoBancarioPessoaJuridicaDTO> extratoDto);
    Task UpdateAsync(ExtratoBancarioPessoaJuridicaDTO extratoDto);
    Task DeleteAsync(int id);
}