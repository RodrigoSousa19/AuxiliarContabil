using AuxiliarContabil.Domain.Interfaces.Repositories;
using AuxiliarContabil.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AuxiliarContabil.Infrastructure.Repositories;

public class ComposicaoSalarialRepository(SqlDbContext context) : Repository<ComposicaoSalario>(context),
    IComposicaoSalarialRepository
{
    public async Task UpdateCurrentComposition(int idComposicaoNova)
    {
        var composicoes = context.ComposicaoSalarios
            .Where(x => x.ComposicaoAtual || x.Id == idComposicaoNova)
            .ToList();

        if (composicoes.Count == 2)
        {
            foreach (var composicao in composicoes)
            {
                composicao.ComposicaoAtual = composicao.Id == idComposicaoNova;
                await UpdateAsync(composicao);
            }
        }
    }
}