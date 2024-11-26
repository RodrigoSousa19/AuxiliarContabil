using AuxiliarContabil.Domain.Dto;

namespace AuxiliarContabil.Domain.Interfaces.Services;

public interface IComposicaoSalarialService
{
    Task<IEnumerable<ComposicaoSalarioDto>> GetAllAsync();
    Task<ComposicaoSalarioDto?> GetByIdAsync(int id);
    Task AddAsync(ComposicaoSalarioDto productDto);
    Task UpdateAsync(ComposicaoSalarioDto productDto);
    Task DeleteAsync(int id);
}