namespace AuxiliarContabil.Domain.Dto;

public class FeriadosDto
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public string? Nome { get; set; }
    public string? Tipo { get; set; }
}