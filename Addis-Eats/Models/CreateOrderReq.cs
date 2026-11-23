using System.ComponentModel.DataAnnotations;
using AddisEats.Api.Validation;

namespace AddisEats.Api.Model;

public class CreateOrderRequest : IValidatableObject
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(120, MinimumLength = 4, ErrorMessage = "Name must be between 4 and 120 characters.")]
    public string Name { get; set; } = string.Empty;

    [Range(1, 100, ErrorMessage = "Item amount needs to be between 1 and 100.")]
    public int Amount { get; set; }

    [EtbAmount]
    public decimal TotalEtb { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "CustomerId must be a valid positive ID.")]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [EthiopianMobile]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "At least one item is required.")]
    [MinLength(1, ErrorMessage = "You must include at least 1 item in the order.")]
    public List<string> Items { get; set; } = new();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Amount < Items.Count)
        {
            yield return new ValidationResult(
                "Total item Amount cannot be less than the number of distinct Items listed.",
                new[] { nameof(Amount), nameof(Items) });
        }
    }
}