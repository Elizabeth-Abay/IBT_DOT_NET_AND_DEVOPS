namespace AddisEats.Api.Model;

public class Order
{
    private static int _orderedItems = 0;

    public int Id { get; private set; } = ++_orderedItems;
    public string Name { get; set; }
    public int Amount { get; set; }
    public decimal TotalEtb { get; set; }
    public int CustomerId { get; set; }
    public string PhoneNumber { get; set; }
    public List<string> Items { get; set; }

    public Order(string name, int amount, decimal totalEtb, int customerId, string phoneNumber, List<string> items)
    {
        Name = name;
        Amount = amount;
        TotalEtb = totalEtb;
        CustomerId = customerId;
        PhoneNumber = phoneNumber;
        Items = items;
    }
}