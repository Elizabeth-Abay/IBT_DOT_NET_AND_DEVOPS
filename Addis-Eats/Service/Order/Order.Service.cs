using AddisEats.Api.Common;
using AddisEats.Api.Data;
using AddisEats.Api.Model;

namespace AddisEats.Api.Service.Order;

public class OrderService : OrderInterface
{
    // Server-side authoritative menu prices (or read from SeedData.menuItems)
    private static readonly Dictionary<string, (decimal PriceEtb, bool InStock)> Menu =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["Doro Wat"] = (480.50m, true),
            ["Kitfo"] = (550.00m, true),
            ["Shiro"] = (220.00m, true),
            ["Tibs"] = (450.00m, false) // Out of stock for testing catalogue code #3
        };

    public Result<Model.Order> Place(CreateOrderRequest request)
    {
        // Catalogue Error #1: Customer does not exist
        if (request.CustomerId <= 0 || request.CustomerId > 1000)
        {
            return Result<Model.Order>.Failure(ErrorCatalogue.CustomerNotFound(request.CustomerId));
        }

        decimal computedTotalEtb = 0m;

        foreach (var itemName in request.Items)
        {
            // Catalogue Error #2: Menu item not found
            if (!Menu.TryGetValue(itemName, out var menuItem))
            {
                return Result<Model.Order>.Failure(ErrorCatalogue.MenuItemNotFound(itemName));
            }

            // Catalogue Error #3: Menu item out of stock
            if (!menuItem.InStock)
            {
                return Result<Model.Order>.Failure(ErrorCatalogue.MenuItemOutOfStock(itemName));
            }

            computedTotalEtb += menuItem.PriceEtb;
        }

        // Scale by Amount if ordering multiples, or sum items directly
        if (request.Items.Count == 1 && request.Amount > 1)
        {
            computedTotalEtb *= request.Amount;
        }

        var createdOrder = new Model.Order(
            request.Name,
            request.Amount,
            computedTotalEtb, // Server-computed, never from client!
            request.CustomerId,
            request.PhoneNumber,
            request.Items
        );

        SeedData.orders.Add(createdOrder);
        return Result<Model.Order>.Success(createdOrder);
    }

    public List<Model.Order> GetOrders(int customerId)
    {
        return SeedData.orders.Where(order => order.CustomerId == customerId).ToList();
    }
}