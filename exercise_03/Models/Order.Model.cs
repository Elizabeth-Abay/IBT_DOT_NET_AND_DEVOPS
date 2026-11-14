namespace AddisEats.Api.Model;

public class Order
{
	static int OrderedItems = 0;
	// id will be set
	private int Id = ++OrderedItems;
	public string Name { get; set; }
	public int Amount { get; set; }
	public decimal Price { get; set; }
	public int CustomerId { get; set; }


	public Order( string Name , int Amount , decimal Price , int CustomerId)
	{
		this.Name = Name;
		this.Amount = Amount;
		this.Price = Price;
		this.CustomerId = CustomerId;

	}

}