namespace AddisEats.Api.Service.Order;

using AddisEats.Api.Model;

using AddisEats.Api.Data;

public sealed record ApiError(int StatusCode, string Code, string Message);


public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public object? Error { get; }

    private Result(bool isSuccess, T? value, object? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(object? error = null) => new(false, default, error);
}

public class OrderService : OrderInterface
{
    // Server-side authoritative menu prices and stock state
    private static readonly Dictionary<string, (decimal PriceEtb, bool InStock)> Menu =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["Doro Wat"] = (480.50m, true),
            ["Kitfo"] = (550.00m, true),
            ["Shiro"] = (220.00m, true),
            ["Tibs"] = (450.00m, false) // Out of stock for testing catalogue error #3
        };



    public Result<AddisEats.Api.Model.Order> Place(AddisEats.Api.Model.CreateOrderRequest request)
    {
        // Catalogue Error #1: Customer does not exist
        if (request.CustomerId <= 0 || request.CustomerId > 1000)
        {
            return Result<AddisEats.Api.Model.Order>.Failure(new ApiError(
            StatusCode: StatusCodes.Status404NotFound,
            Code: "CUSTOMER.NOT_FOUND",
            Message: $"Customer with ID {request.CustomerId} does not exist."
        ));
        }

        decimal computedTotalEtb = 0m;

        foreach (var itemName in request.Items)
        {
            // Catalogue Error #2: Menu item not found
            if (!Menu.TryGetValue(itemName, out var menuItem))
            {
                return Result<AddisEats.Api.Model.Order>.Failure(new ApiError(
            StatusCode: StatusCodes.Status404NotFound,
            Code: "CUSTOMER.NOT_FOUND",
            Message: $"Customer with ID {request.CustomerId} does not exist."
        ));
            }

            // Catalogue Error #3: Menu item out of stock
            if (!menuItem.InStock)
            {
                return Result<AddisEats.Api.Model.Order>.Failure(new ApiError(
            StatusCode: StatusCodes.Status404NotFound,
            Code: "CUSTOMER.NOT_FOUND",
            Message: $"Customer with ID {request.CustomerId} does not exist."
        ));
            }

            computedTotalEtb += menuItem.PriceEtb;
        }

        // Scale total when a single item type is ordered in multiple quantities
        if (request.Items.Count == 1 && request.Amount > 1)
        {
            computedTotalEtb *= request.Amount;
        }

        var order = new AddisEats.Api.Model.Order(
            name: request.Name,
            amount: request.Amount,
            totalEtb: computedTotalEtb, // Server-computed, never trusted from client payload
            customerId: request.CustomerId,
            phoneNumber: request.PhoneNumber,
            items: new List<string>(request.Items)
        );

        SeedData.orders.Add(order);
        return Result<AddisEats.Api.Model.Order>.Success(order);
    }

    public List<AddisEats.Api.Model.Order> GetOrders(int customerId)
    {
        return SeedData.orders
            .Where(order => order.CustomerId == customerId)
            .ToList();
    }
}
