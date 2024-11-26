using System;
using System.Collections.Generic;

namespace AuxiliarContabil.Infrastructure;

public partial class ExtratoBancarioPessoaJuridica
{
    public int Id { get; set; }

    public string NomeBanco { get; set; } = null!;

    public DateOnly DataTransacao { get; set; }

    public string TipoTransacao { get; set; } = null!;

    public decimal ValorTransacao { get; set; }

    public string Descricao { get; set; } = null!;
}
