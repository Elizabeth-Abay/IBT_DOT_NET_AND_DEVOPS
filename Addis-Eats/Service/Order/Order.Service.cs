namespace AddisEats.Api.Service.Order;

using AddisEats.Api.Data;

using AddisEats.Api.Model;


public class OrderService : OrderInterface
{
    public bool SetOrder(Order createdOrder)
    {
        // string Name, int Amount, decimal Price, int CustomerId
        // set up the static array in SeedData
        //Order createdOrder = new Order(string Name, int Amount, decimal Price, int CustomerId);

        try
        {
            SeedData.orders.Add(createdOrder);
            return true;

        } catch (Exception exc)
        {
            Console.WriteLine($"Error happened while setting up orders {exc}");
            return false;
        }

    }

    public List<AddisEats.Api.Model.Order>? GetOrders(int CustomerId)
    {
        // select and send orders
        var returnedMatches = SeedData.orders.Where(order => order.CustomerId == CustomerId).ToList();

        return returnedMatches;


    }

}