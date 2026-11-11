namespace AddisEats.Api.Mappers;


using  AddisEats.Api.Model;
using  AddisEats.Api.DTOs;


// static class ??
// automapper ?? = to be read package
public static class RestaurantMapper
{
    // so we can do restaurantObj.ToDto()
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