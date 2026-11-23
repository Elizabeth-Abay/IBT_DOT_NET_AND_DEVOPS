using Microsoft.AspNetCore.Mvc;

using AddisEats.Api.Service.Order;

using AddisEats.Api.Model;

namespace AddisEats.Api.Controller;


[ApiController]
[Route("/order")]
public class OrderController : ControllerBase
{
    private readonly OrderInterface _orderService;

    public OrderController(OrderInterface orderServuce)
    {
        _orderService = orderServuce;
    }

    [HttpGet]
    public ActionResult GetAll([FromQuery] int CustomerId)
    {
        List<Order>? ordersFound = _orderService.GetOrders(CustomerId);

        if (ordersFound.Count == 0) return NotFound();

        return Ok(ordersFound);
    }


    [HttpPost]
    public ActionResult CreateOrder([FromBody] Order body)
    {
        bool result = _orderService.SetOrder(body);

        return Ok(result);
    }
}


