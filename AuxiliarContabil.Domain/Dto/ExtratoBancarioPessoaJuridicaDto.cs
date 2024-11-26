namespace AuxiliarContabil.Domain.Dto;

public class ExtratoBancarioPessoaJuridicaDTO
{
    public int Id { get; set; }
    public string NomeBanco { get; set; }
    public DateTime DataTransacao { get; set; }
    public string TipoTransacao { get; set; }
    public decimal ValorTransacao { get; set; }
    public string Descricao { get; set; }
}