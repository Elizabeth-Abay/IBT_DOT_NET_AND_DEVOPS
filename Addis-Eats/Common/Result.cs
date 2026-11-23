namespace AddisEats.Api.Common;

public sealed record Error(string Code, string Message, int StatusCode)
{
    public static readonly Error None = new(string.Empty, string.Empty, 200);
}

public sealed class Result<T>
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

public static class CatalogueErrors
{
    public static Error ValidationFailed() =>
        new("VALIDATION.FAILED", "One or more validation errors occurred.", StatusCodes.Status400BadRequest);

    public static Error MenuItemNotFound(int id) =>
        new("MENU_ITEM.NOT_FOUND", $"Menu item {id} was not found.", StatusCodes.Status404NotFound);

    public static Error MenuItemUnavailable(int id) =>
        new("MENU_ITEM.UNAVAILABLE", $"Menu item {id} is currently unavailable.", StatusCodes.Status409Conflict);

    public static Error OrderNotFound(int id) =>
        new("ORDER.NOT_FOUND", $"Order {id} was not found.", StatusCodes.Status404NotFound);

    public static Error RestaurantNotFound(int id) =>
        new("RESTAURANT.NOT_FOUND", $"Restaurant {id} was not found.", StatusCodes.Status404NotFound);

    public static Error InvalidOrderState(string reason) =>
        new("ORDER.INVALID_STATE", reason, StatusCodes.Status422UnprocessableEntity);

    public static Error DuplicateEntry(string resource) =>
        new("RESOURCE.DUPLICATE", $"{resource} already exists.", StatusCodes.Status409Conflict);

    public static Error UnexpectedError() =>
        new("SERVER.UNEXPECTED_ERROR", "An unexpected error occurred while processing the request.", StatusCodes.Status500InternalServerError);
}