using  AddisEats.Api.DTOs; // dto for restaurants

using  AddisEats.Api.Model; // for the menu items

namespace AddisEats.Api.Services.Restaurant;



public interface RestaurantServiceInterface
{

    public AddisEats.Api.Model.Restaurant? GetById(int Id);

    
    // pass default values when it is called
    public List<AddisEats.Api.Model.Restaurant> GetAll(string? SearchArea , bool IsActive , int page , int pageSize);


    //public List<MenuItem> GetMenuForRestaurant(int RestaurantId);
}