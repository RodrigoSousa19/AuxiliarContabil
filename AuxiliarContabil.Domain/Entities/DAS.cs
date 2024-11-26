using System;
using System.Collections.Generic;

namespace AuxiliarContabil.Infrastructure;

public partial class DAS
{
    public int Faixa { get; set; }

    public decimal? ReceitaBrutaAnual { get; set; }

    public decimal? Aliquota { get; set; }
}
