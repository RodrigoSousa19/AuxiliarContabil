namespace AuxiliarContabil.Infrastructure;

public partial class ComposicaoSalario : BaseEntity{
    public DateTime InicioPeriodo { get; set; }

    public DateTime FimPeriodo { get; set; }

    public int QuantidadeDiasUteis { get; set; }

    public decimal Das { get; set; }

    public decimal Gps { get; set; }

    public decimal ProLabore { get; set; }

    public decimal? SalarioHora { get; set; }

    public decimal? SalarioDia { get; set; }

    public decimal? SalarioBruto { get; set; }

    public decimal? MensalidadeContabilidade { get; set; }
    public bool ComposicaoAtual { get; set; }
}
