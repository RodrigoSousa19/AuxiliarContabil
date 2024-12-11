namespace AuxiliarContabil.Domain.Models;

public class Transacao
{
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
    public DateTime Date { get; set; }
}