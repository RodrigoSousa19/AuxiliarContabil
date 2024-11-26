using System;
using System.Collections.Generic;

namespace AuxiliarContabil.Infrastructure;

public partial class Feriado
{
    public DateOnly Data { get; set; }

    public string? Nome { get; set; }

    public string? Tipo { get; set; }
}
