﻿using System;
using System.Collections.Generic;

namespace AuxiliarContabil.Infrastructure;

public partial class DAS : BaseEntity
{
    public string Faixa { get; set; }

    public decimal? ReceitaBrutaAnual { get; set; }

    public decimal? Aliquota { get; set; }
}
