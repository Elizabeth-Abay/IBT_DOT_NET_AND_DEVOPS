using  AddisEats.Api.DTOs; // signature

using  AddisEats.Api.Mappers; // service is the one that will do the mapping 

using  AddisEats.Api.Model; // for accessing the Restaurant

using  AddisEats.Api.Data;  // is the data source 


// when it is filtered then do the mapping
namespace AddisEats.Api.Services.Restaurant;



public class RestaurantService : RestaurantServiceInterface
{
    private static readonly List<AddisEats.Api.Model.Restaurant> allRestaurants = SeedData.restaurants;
    private static readonly List<MenuItem> allMenu = SeedData.menuItems;

    // pagination = skip =( pageWanted - 1 ) * pageSize
    // take = pageSize
    public RestaurantService() { }

    public RestaurantDTO? GetById(int Id)
    {
        AddisEats.Api.Model.Restaurant? restaurant = allRestaurants.FirstOrDefault(x => x.Id == Id);


        if (restaurant == null) return null;

        // but here we need to map the restaurant to dto
        RestaurantDTO restaurantMapped = restaurant.ToDto();

        return restaurantMapped;

    }

    public List<RestaurantDTO> GetAll(string? SearchArea, bool IsActive, int page, int pageSize)
    {
        // match the search Area and bool , order it , skip the page size
        //(SearchArea == null && x.IsActive == IsActive) || (x.Address.ToLower() == SearchArea.ToLower() && ) )  
        List<AddisEats.Api.Model.Restaurant> matchedItems = allRestaurants.Where( x => ( x.IsActive == IsActive && (string.IsNullOrEmpty(SearchArea) || x.Address.ToLower() == SearchArea.ToLower() ) ))
                                                             .Skip((page - 1) * pageSize)
                                                            .Take(pageSize)
                                                            .ToList();

        List<RestaurantDTO> finalReturned = new List<RestaurantDTO> { };
      

        foreach (AddisEats.Api.Model.Restaurant matched in matchedItems)
        {
            finalReturned.Add(matched.ToDto());

        }


        // searchTerm - null then only filter with the isActive
        // else use the search area and the isActive

        return finalReturned;
    }


    public List<MenuItem> GetMenuForRestaurant(int RestaurantId)
    {
        // filter the menuItems based on the restaurant id
        List<MenuItem> returnedMenuItems = allMenu.Where(x => x.RestaurantId == RestaurantId).ToList();

        return returnedMenuItems;

    }


}