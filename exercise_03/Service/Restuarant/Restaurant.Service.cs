using  AddisEats.Api.DTOs; // signature

using  AddisEats.Api.Mappers; // service is the one that will do the mapping

using  AddisEats.Api.Model; // for accessing the Restaurant

using  AddisEats.Api.Data;  // is the data source 
// when it is filtered then do the mapping

namespace AddisEats.Api.Services.Restaurant;



public class RestaurantService : RestaurantServiceInterface
{
    private readonly List<AddisEats.Api.Model.Restaurant> allRestaurants = SeedData.restaurants;

    // pagination = skip =( pageWanted - 1 ) * pageSize
    // take = pageSize


    public AddisEats.Api.Model.Restaurant? GetById(int Id)
    {
        AddisEats.Api.Model.Restaurant? restaurant = allRestaurants.FirstOrDefault(x => x.Id == Id);

        return restaurant;

    }

    public List<AddisEats.Api.Model.Restaurant> GetAll(string? SearchArea, bool IsActive, int page, int pageSize)
    {
        // match the search Area and bool , order it , skip the page size
        List<AddisEats.Api.Model.Restaurant> matchedItems = allRestaurants.Where(x => (SearchArea == null && x.IsActive == IsActive) ||  (x.Address.ToLower() == SearchArea.ToLower() && x.IsActive == IsActive))
            .Skip((page - 1)  * pageSize)
            .Take(pageSize)
            .ToList();

        // searchTerm - null then only filter with the isActive
        // else use the search area and the isActive

        return matchedItems;
    }

   
    //public List<MenuItem> GetMenuForRestaurant(int RestaurantId)
    //{

    //}


}