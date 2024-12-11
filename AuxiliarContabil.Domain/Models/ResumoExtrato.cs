namespace AuxiliarContabil.Domain.Models;

public class ResumoExtrato
{
    public ResumoExtrato()
    {
        Movimentacoes = new List<MovimentacaoExtrato>();
    }
    
    public string Banco { get; set; }
    public IEnumerable<MovimentacaoExtrato> Movimentacoes { get; set; }
}