using Microsoft.AspNetCore.Mvc;
// here is where the annotations reside

using  AddisEats.Api.Services.MenuItems;

using AddisEats.Api.Model;


namespace AddisEats.Api.Controller;


// controllers implement the interface ControllerBase
[ApiController] // it is a controller
// we dont have to manually check model state binding and validation err bc of [ApiController]
[Route("api/v1/menu")] // this controller is called for this route
public class MenuItemsController : ControllerBase
{
    private readonly MenuItemService _menuItemService;

    MenuItemsController(MenuItemService _menuItemService)
    {
        // dependency injection for the items
        this._menuItemService = _menuItemService;
    }

    // in the controller get , post , put , delete
    // specify that


    [HttpGet]
    // if u put [HttpGet("active")] - parent + active
    // if u put [HttpGet("/active")] - parent dropped only /active
    List<MenuItem> getAll()
    {
        return _menuItemService.getAll();
    }


    [HttpGet]
    [Route("/{id}")]
    // this Id is received from the path parameter
    MenuItem getOne([FromRoute]  int Id)
    {
        return _menuItemService.getOne(Id);
    }

}


// Return types in controllers
// Specific types
// multiple return types are available - then ActionResult<T>
// IActionResult  ???
// IEnumerable<T> or IAsyncEnumerable<T>

// Response formatter -- what is it ?