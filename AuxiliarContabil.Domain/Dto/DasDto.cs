namespace AuxiliarContabil.Domain.Dto;

public class DasDto
{
    public int Id { get; set; }
    public string Faixa { get; set; }
    public decimal ReceitaBrutaAnual { get; set; }
    public decimal Aliquota { get; set; }
}