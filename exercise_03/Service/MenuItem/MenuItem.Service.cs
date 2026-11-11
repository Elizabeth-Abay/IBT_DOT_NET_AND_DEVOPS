using AddisEats.Api.Model;

namespace AddisEats.Api.Services.MenuItems;

public class MenuItemService : MenuItemsServiceInterface
{
    private List<MenuItem> _menuItems = new List<MenuItem>
    {
        new MenuItem()
        {
            Id = 1,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
        },
        new MenuItem()
        {
            Id = 2,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
        },
        new MenuItem()
        {
            Id = 3,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
        },
        new MenuItem()
        {
            Id = 4,
            Name = "Doro Wot",
            Category = "Wot",
            Price = 1200m,
        },
        new MenuItem()
        {
            Id = 5,
            Name = "Doro Wetrrre",
            Category = "Tibss",
            Price = 2300m
        }
    };

    public List<MenuItem> getAll()
    {
        return _menuItems;
    }

    public MenuItem? getOne(int id)
    {
        MenuItem? item = _menuItems.FirstOrDefault(x => x.Id == id);
        return item;
    }
}