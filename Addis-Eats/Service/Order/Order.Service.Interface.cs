using AddisEats.Api.Common;
using AddisEats.Api.Model;

namespace AddisEats.Api.Service.Order;

public interface OrderInterface
{
    Result<Model.Order> Place(CreateOrderRequest request);
    List<Model.Order> GetOrders(int customerId);
}