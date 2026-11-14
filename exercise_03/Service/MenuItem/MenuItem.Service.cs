using AddisEats.Api.Model;

using AddisEats.Api.Data;

namespace AddisEats.Api.Services.MenuItems;

public class MenuItemService : MenuItemsServiceInterface
{
    private static readonly List<MenuItem> _menuItems = SeedData.menuItems;

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