namespace AddisEats.Api.Common;

public sealed record Error(string Code, string Message, int StatusCode = 400)
{
    public static readonly Error None = new(string.Empty, string.Empty, 200);
}

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public Error Error { get; }

    private Result(bool isSuccess, T? value, Error error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new(true, value, Error.None);
    public static Result<T> Failure(Error error) => new(false, default, error);
}

public static class ErrorCatalogue
{
    // 1-3: Returned from IOrderService.Place
    public static Error CustomerNotFound(int customerId) =>
        new("ORDER.CUSTOMER_NOT_FOUND", $"Customer with ID {customerId} does not exist.", 404);

    public static Error MenuItemNotFound(string itemName) =>
        new("ORDER.MENU_ITEM_NOT_FOUND", $"Menu item '{itemName}' is not on the AddisEats menu.", 404);

    public static Error MenuItemOutOfStock(string itemName) =>
        new("ORDER.MENU_ITEM_OUT_OF_STOCK", $"Menu item '{itemName}' is currently out of stock.", 409);

    // 4-8: Additional catalogue codes for Exercise 8
    public static Error OrderNotFound(int orderId) =>
        new("ORDER.NOT_FOUND", $"Order #{orderId} was not found.", 404);

    public static Error DuplicateOrder() =>
        new("ORDER.DUPLICATE", "An identical order was just placed by this customer.", 409);

    public static Error MinimumOrderNotMet(decimal minEtb) =>
        new("ORDER.MINIMUM_NOT_MET", $"Minimum order total is {minEtb} ETB.", 422);

    public static Error ValidationFailed(string detail) =>
        new("REQUEST.VALIDATION_FAILED", detail, 400);

    public static Error UnexpectedFailure(string traceId) =>
        new("SERVER.INTERNAL_ERROR", $"An unexpected error occurred. TraceId: {traceId}", 500);
}