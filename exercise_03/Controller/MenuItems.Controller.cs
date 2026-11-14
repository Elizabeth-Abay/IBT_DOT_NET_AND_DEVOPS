using Microsoft.AspNetCore.Mvc;
// here is where the annotations reside

using  AddisEats.Api.Services.MenuItems;

using AddisEats.Api.Model;


namespace AddisEats.Api.Controller;


// we dont have to manually check model state binding and validation err bc of [ApiController]
// controllers implement the interface ControllerBase
[ApiController] // it is a controller
[Route("api/v1/menu")] // this controller is called for this route
public class MenuItemsController : ControllerBase
{
    private readonly MenuItemsServiceInterface _menuItemService;

    public MenuItemsController(MenuItemsServiceInterface menuItemService)
    {
        // dependency injection for the items
           _menuItemService = menuItemService;
    }

    // in the controller get , post , put , delete
    // specify that


    [HttpGet]
    public List<MenuItem> getAll()
    {
        return _menuItemService.getAll();
    }


    [HttpGet]
    [Route("{id}")]
    public MenuItem getOne([FromRoute]  int Id)
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