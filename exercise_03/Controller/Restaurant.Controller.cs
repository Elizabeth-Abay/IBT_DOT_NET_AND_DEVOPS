// this is the file that accesses the requests
// annotations

using Microsoft.AspNetCore.Mvc;
using AddisEats.Api.Services.Restaurant; // the namespace for the interface

using  AddisEats.Api.Model;

using  AddisEats.Api.DTOs;

namespace AddisEats.Api.Controller.Restaurant;




[ApiController]
[Route("/api/v1/restaurant")]
public class RestaurantController : ControllerBase
{
    private readonly RestaurantServiceInterface _myRestaurantService;
    // dependency injection
    // we use the service interface
    public RestaurantController(RestaurantServiceInterface InjectedRestaurantService)
    {
        _myRestaurantService = InjectedRestaurantService;
    }


    // all
    [HttpGet]
    //List<AddisEats.Api.Model.Restaurant>
    public ActionResult GetAll(
        [FromQuery] string? SearchArea,
        [FromQuery] bool IsActive = true, // default is true
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 6

    )
    {
        // we need model binding here
        // then pass that to the service
       var items = _myRestaurantService.GetAll(SearchArea, IsActive, page, pageSize);


       return Ok(items);
    }


    [HttpGet]
    [Route("{id}")]
    public ActionResult? GetById([FromRoute] int id)
    {
        RestaurantDTO? rest = _myRestaurantService.GetById(id);

        if (rest == null) return NotFound();

        return Ok(rest);
    }


    [HttpGet]
    [Route("{id}/menu")]
    public ActionResult GetMenuRestaurant([FromRoute] int id)
    {
        List<MenuItem> result = _myRestaurantService.GetMenuForRestaurant(id);

        if (result == null) return NotFound();

        return Ok(result);
    }

}