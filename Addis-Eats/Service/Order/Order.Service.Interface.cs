using AddisEats.Api.Common;
using AddisEats.Api.Model;

namespace AddisEats.Api.Service.Order;

public interface OrderInterface
{
    public Result<AddisEats.Api.Model.Order> Place(AddisEats.Api.Model.CreateOrderRequest request);
    public List<AddisEats.Api.Model.Order> GetOrders(int customerId);
}