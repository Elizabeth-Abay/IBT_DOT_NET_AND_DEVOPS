using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AddisEats.Api.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed partial class EthiopianMobileAttribute : ValidationAttribute
{
    [GeneratedRegex(@"^(?:0[79]\d{8}|\+251[79]\d{8})$")]
    private static partial Regex EthiopianMobileRegex();

    public EthiopianMobileAttribute()
    {
        ErrorMessage = "PhoneNumber must be a valid Ethiopian mobile number (09..., 07..., +2519..., or +2517...).";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success;

        if (value is string phone && EthiopianMobileRegex().IsMatch(phone.Trim()))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}