using System.ComponentModel.DataAnnotations;

namespace Cards.Validators;
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class StatusValidator : ValidationAttribute
{
    private readonly string[] allowedValues;

    public StatusValidator(params string[] allowedValues)
    {
        this.allowedValues = allowedValues ?? throw new ArgumentNullException(nameof(allowedValues));
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null && Array.IndexOf(allowedValues, value.ToString()) == -1)
        {
            return new ValidationResult($"The field {validationContext.DisplayName} must have one of the following values: {string.Join(", ", allowedValues)}.");
        }

        return ValidationResult.Success;
    }
}
