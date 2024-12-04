using AuxiliarContabil.Domain.Dto;

namespace AuxiliarContabil.Domain.Interfaces.Services;

public interface IFeriadosService
{
    Task<IEnumerable<FeriadosDto>> GetAllAsync();
}