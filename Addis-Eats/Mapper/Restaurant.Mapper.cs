namespace AddisEats.Api.Mappers;


using  AddisEats.Api.Model;
using  AddisEats.Api.DTOs;


// static class - are classes that can't be used with the new key word
// it shld only contain static members - methods and data
// contains static constructor implicitly 
// automapper ?? = to be read package
public static class RestaurantMapper
{
    // so we can do restaurantObj.ToDto()
    // this is an extension method for calling ToDto onto a restaurant obj
    public static RestaurantDTO ToDto(this Restaurant restaurant)
    {
        return new RestaurantDTO(
            restaurant.Id,
            restaurant.Name,
            restaurant.Address,
            restaurant.Phone,
            restaurant.IsActive
        );
    }
}