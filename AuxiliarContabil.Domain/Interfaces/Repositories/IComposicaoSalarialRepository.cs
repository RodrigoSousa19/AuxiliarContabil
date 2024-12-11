using AuxiliarContabil.Domain.Entities;

namespace AuxiliarContabil.Domain.Interfaces.Repositories;

public interface IComposicaoSalarialRepository : IRepository<ComposicaoSalario>
{
    Task UpdateCurrentComposition(int idComposicaoNova);
}