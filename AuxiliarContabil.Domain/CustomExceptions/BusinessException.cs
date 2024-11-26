namespace AuxiliarContabil.API.CustomExceptions;

public class BusinessException(IEnumerable<ValidationError> errors) : Exception
{
    public List<ValidationError> Errors { get; } = errors.ToList();

    public override string Message => "Uma ou mais validações falharam.";
}

public class ValidationError(string message, string field)
{
    public string Message { get; set; } = message;
    public string Field { get; set; } = field;
}