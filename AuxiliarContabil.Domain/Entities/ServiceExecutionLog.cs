using System;
using System.Collections.Generic;

namespace AuxiliarContabil.Infrastructure;

public partial class ServiceExecutionLog
{
    public int LogId { get; set; }

    public int ServiceId { get; set; }

    public DateTime StartTime { get; set; }

    public int Status { get; set; }

    public DateTime? EndTime { get; set; }

    public string? ExceptionMessage { get; set; }

    public virtual ScheduledService Service { get; set; } = null!;
}
