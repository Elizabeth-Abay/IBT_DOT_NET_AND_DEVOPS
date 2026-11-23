using System.ComponentModel.DataAnnotations;

namespace AddisEats.Api.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class EtbAmountAttribute : ValidationAttribute
{
    public EtbAmountAttribute()
    {
        ErrorMessage = "ETB amount must be greater than 0 and have at most 2 decimal places.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success;

        if (value is not decimal amount)
        {
            return new ValidationResult("Amount must be a decimal number.");
        }

        if (amount <= 0m)
        {
            return new ValidationResult("Amount cannot be zero or negative.");
        }

        if (decimal.Truncate(amount * 100m) != amount * 100m)
        {
            return new ValidationResult("Amount cannot have more than two decimal places.");
        }

        return ValidationResult.Success;
    }
}