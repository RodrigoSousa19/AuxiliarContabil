namespace AuxiliarContabil.Domain.Models;

public class MovimentacaoExtrato
{
    public MovimentacaoExtrato()
    {
        Transacoes = new List<Transacao>();
    }
    
    public string Tipo { get; set; }
    public IEnumerable<Transacao> Transacoes { get; set; }
}