using AuxiliarContabil.Domain.Dto;

namespace AuxiliarContabil.Domain.Interfaces.Services;

public interface IFeriadosService
{
    Task<IEnumerable<FeriadosDto>> GetAllAsync();
    Task<IEnumerable<FeriadosDto>> GetByDateRange(DateTime startDate, DateTime endDate);
}