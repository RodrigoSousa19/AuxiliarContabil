namespace AuxiliarContabil.Domain.Dto;

public class ScheduledServicesDto
{
    public int Id { get; set; }
    public string ServiceName { get; set; }
    public string ClassName { get; set; }
    public string CronExpression { get; set; }
}