// get and set orders

namespace AddisEats.Api.Service.Order;

using AddisEats.Api.Model;


public interface OrderInterface
{
	public bool SetOrder(Order newOrder);
	//string Name, int Amount, decimal Price , int CustomerId

	public List<AddisEats.Api.Model.Order> GetOrders(int CustomerId);

}