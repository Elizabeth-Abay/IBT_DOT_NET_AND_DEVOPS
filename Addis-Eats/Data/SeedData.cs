// seed file is used to put data into ur table

using AddisEats.Api.Model;

namespace AddisEats.Api.Data;


// this resembles the data we get from the database
public class SeedData
{
    public static List<Restaurant> restaurants = new List<Restaurant>
    {

        new Restaurant { Id = 1, Name = "Addis Ababa Restaurant", Address = "Bole", Phone = "555-1234", IsActive = true, OwnerAddress = "456 Elm St" },
        new Restaurant { Id = 2, Name = "Blue Nile Restaurant", Address = "Ayat", Phone = "555-5678", IsActive = false, OwnerAddress = "789 Pine St" },
        new Restaurant { Id = 3, Name = "Lucy Restaurant", Address = "Mexico", Phone = "555-9012", IsActive = true, OwnerAddress = "321 Cedar St" },
        new Restaurant { Id = 4, Name = "Ethio Restaurant", Address = "Megenagna", Phone = "555-3456", IsActive = true, OwnerAddress = "654 Spruce St" },
        new Restaurant { Id = 5, Name = "Tena Yistilign Restaurant", Address = "Bole", Phone = "555-7890", IsActive = true, OwnerAddress = "987 Birch St" },
        new Restaurant { Id = 6, Name = "Kategna Restaurant", Address = "Megenagna", Phone = "555-2345", IsActive = true, OwnerAddress = "123 Main St" },
        new Restaurant { Id = 7, Name = "Sishu Restaurant", Address = "Bole", Phone = "555-6789", IsActive = false, OwnerAddress = "246 Aspen St" },
        new Restaurant { Id = 8, Name = "Abay Restaurant", Address = "Megenagna", Phone = "555-0123", IsActive = true, OwnerAddress = "357 Cherry St" },
        new Restaurant { Id = 9, Name = "Fendika Restaurant", Address = "Mexico", Phone = "555-4567", IsActive = false, OwnerAddress = "468 Walnut St" },
        new Restaurant { Id = 10, Name = "Doro Wat Restaurant", Address = "Megenagna", Phone = "555-8901", IsActive = true, OwnerAddress = "579 Chestnut St" },
        new Restaurant { Id = 11, Name = "Injera Restaurant", Address = "Mexico", Phone = "555-2345", IsActive = true, OwnerAddress = "680 Poplar St" },
        new Restaurant { Id = 12, Name = "Bunna Restaurant", Address = "Ayat", Phone = "555-6789", IsActive = true, OwnerAddress = "791 Fir St" }
    };



    public static List<MenuItem> menuItems = new List<MenuItem>
    {
        new MenuItem()
        {
            Id = 1,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 4
        },
        new MenuItem()
        {
            Id = 2,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 5
        },
        new MenuItem()
        {
            Id = 3,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 5
        },
        new MenuItem()
        {
            Id = 4,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 5
        },
        new MenuItem()
        {
            Id = 5,
            Name = "Doro Wetrrre",
            Category = "Tibss",
            Price = 2300m,
            RestaurantId = 1
        },
        new MenuItem()
        {
            Id = 6,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 1
        },
        new MenuItem()
        {
            Id = 7,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 2
        },
        new MenuItem()
        {
            Id = 8,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 2
        },
        new MenuItem()
        {
            Id = 9,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 3
        },
        new MenuItem()
        {
            Id = 10,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 6
        },
        new MenuItem()
        {
            Id = 11,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 7
        },
        new MenuItem()
        {
            Id = 12,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 7
        },
        new MenuItem()
        {
            Id = 13,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 8
        },
        new MenuItem()
        {
            Id = 14,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 9
        },
        new MenuItem()
        {
            Id = 15,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 9
        },
        new MenuItem()
        {
            Id = 16,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 10
        },
        new MenuItem()
        {
            Id = 17,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 10
        },
        new MenuItem()
        {
            Id = 18,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 11
        },
        new MenuItem()
        {
            Id = 19,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 11
        },
        new MenuItem()
        {
            Id = 20,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 12
        },
        new MenuItem()
        {
            Id = 21,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 12
        },
        new MenuItem()
        {
            Id = 22,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 3
        },
         new MenuItem()
        {
            Id = 23,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 2
        },
         new MenuItem()
           {
            Id = 24,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
            RestaurantId = 1
        },

    };


    public static List<Order> orders = new List<Order> {
    };

}
