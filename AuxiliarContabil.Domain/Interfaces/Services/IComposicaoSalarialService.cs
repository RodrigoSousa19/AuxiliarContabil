using AuxiliarContabil.Domain.Dto;

namespace AuxiliarContabil.Domain.Interfaces.Services;

public interface IComposicaoSalarialService
{
    Task<IEnumerable<ComposicaoSalarioDto>> GetAllAsync();
    Task<ComposicaoSalarioDto?> GetByIdAsync(int id);
    Task AddAsync(ComposicaoSalarioDto composicaoDto);
    Task UpdateAsync(ComposicaoSalarioDto composicaoDto);
    Task DeleteAsync(int id);
    Task UpdateCurrentComposition(int id);
}