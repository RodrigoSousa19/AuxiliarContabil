using System;
using System.Collections.Generic;

namespace AuxiliarContabil.Domain.Entities;

public partial class Feriado : BaseEntity
{
    public DateTime Data { get; set; }

    public string? Nome { get; set; }

    public string? Tipo { get; set; }
}
