namespace AuxiliarContabil.Domain.Dto;

public class ServiceExecutionLogDto
{
    public int LogId { get; set; }
    public int ServiceId { get; set; }
    public DateTime StartTime { get; set; }
    public int Status { get; set; } // 1 = Success, 2 = Failed, 3 = Running (exemplo)
    public DateTime? EndTime { get; set; }
    public string ExceptionMessage { get; set; }
}