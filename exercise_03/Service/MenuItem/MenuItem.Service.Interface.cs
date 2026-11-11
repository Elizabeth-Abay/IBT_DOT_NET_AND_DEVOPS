namespace AddisEats.Api.Services.MenuItems;

using AddisEats.Api.Model;


public interface MenuItemsServiceInterface
{
    // defines the get all and get by id methods
    List<MenuItem> getAll();

    MenuItem getOne(int Id);
}