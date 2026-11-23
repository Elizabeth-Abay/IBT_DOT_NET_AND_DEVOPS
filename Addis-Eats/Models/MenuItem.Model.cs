namespace AddisEats.Api.Model;

// this is the class for the database items definition
// this is the entity class

public class MenuItem
{
    // using private int Id { get; set; } - makes the setter , getter private as well
    private const decimal VatRate = 0.15m;
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int RestaurantId { get; set; }
    public decimal PriceWithVat => Price + Price * VatRate;

    public MenuItem() { }

    public MenuItem(int Id , string Name, string Category , decimal Price , int RestaurantId)
    {
        this.Id = Id;
        this.Name = Name;
        this.Category = Category;
        this.Price = Price;
        this.RestaurantId = RestaurantId;

    }
}