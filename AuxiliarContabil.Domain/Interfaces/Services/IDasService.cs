using AuxiliarContabil.Domain.Dto;

namespace AuxiliarContabil.Domain.Interfaces.Services;

public interface IDasService
{
    Task<IEnumerable<DasDto>> GetAllAsync();
    Task<DasDto?> GetByIdAsync(int id);
    Task AddAsync(DasDto dasDto);
    Task UpdateAsync(DasDto dasDto);
    Task DeleteAsync(int id);
}