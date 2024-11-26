using System;
using System.Collections.Generic;

namespace AuxiliarContabil.Infrastructure;

public partial class ScheduledService
{
    public int Id { get; set; }

    public string ServiceName { get; set; } = null!;

    public string ClassName { get; set; } = null!;

    public string CronExpression { get; set; } = null!;

    public virtual ICollection<ServiceExecutionLog> ServiceExecutionLogs { get; set; } = new List<ServiceExecutionLog>();
}
