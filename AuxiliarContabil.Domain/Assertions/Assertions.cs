using AuxiliarContabil.API.CustomExceptions;

namespace AuxiliarContabil.API.Assertions;

public static class Assertions
{
    private static readonly List<ValidationError> _errors = new();

    public static void ClearErrors() => _errors.Clear();

    public static void Validate()
    {
        if (_errors.Any())
        {
            var errorsToThrow = new List<ValidationError>(_errors);
            ClearErrors();
            throw new BusinessException(errorsToThrow);
        }
    }

    public static void IsValidDate(DateTime? value, string fieldName)
    {
        if (!value.HasValue || value.Value == default)
        {
            _errors.Add(new ValidationError($"O campo {fieldName} contém uma data inválida.", fieldName));
        }
    }

    public static void IsGreaterThanZero(decimal value, string fieldName)
    {
        if (value <= 0)
        {
            _errors.Add(new ValidationError($"O campo {fieldName} deve ser maior que zero.", fieldName));
        }
    }

    public static void IsInRange(DateTime value, DateTime min, DateTime max, string fieldName)
    {
        if (value < min || value > max)
        {
            _errors.Add(new ValidationError($"O campo {fieldName} deve estar entre {min:dd/MM/yyyy} e {max:dd/MM/yyyy}.", fieldName));
        }
    }

}