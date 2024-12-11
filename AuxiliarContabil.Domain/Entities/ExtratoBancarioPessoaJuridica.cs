using System;
using System.Collections.Generic;

namespace AuxiliarContabil.Domain.Entities;

public partial class ExtratoBancarioPessoaJuridica : BaseEntity
{
    public string NomeBanco { get; set; } = null!;

    public DateTime DataTransacao { get; set; }

    public string TipoTransacao { get; set; } = null!;

    public decimal ValorTransacao { get; set; }

    public string Descricao { get; set; } = null!;
}
