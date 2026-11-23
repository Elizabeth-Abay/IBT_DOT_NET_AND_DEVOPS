namespace AddisEats.Api.DTOs;


// record is a value based equality checker
// but class -> object is reference based equality
public record RestaurantDTO
(
    // names in the class and record gotta be the same
    int Id,
    string Name ,
    string Address ,
    string Phone ,
    bool IsActive
);

