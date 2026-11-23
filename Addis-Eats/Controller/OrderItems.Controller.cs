using Microsoft.AspNetCore.Mvc;
using AddisEats.Api.Service.Order;
using AddisEats.Api.Model;

namespace AddisEats.Api.Controller;

[ApiController]
[Route("/order")]
public class OrderController : ControllerBase
{
    private readonly OrderInterface _orderService;

    public OrderController(OrderInterface orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public ActionResult GetAll([FromQuery] int customerId)
    {
        var ordersFound = _orderService.GetOrders(customerId);
        if (ordersFound.Count == 0) return NotFound();

        return Ok(ordersFound);
    }

    [HttpPost]
    public ActionResult CreateOrder([FromBody] CreateOrderRequest body)
    {
        var result = _orderService.Place(body);

        if (!result.IsSuccess)
        {
            return StatusCode(result.Error.StatusCode, new
            {
                code = result.Error.Code,
                message = result.Error.Message
            });
        }

        return Ok(result.Value);
    }

    [HttpGet("boom")]
    public ActionResult DeliberateThrow()
    {
        throw new InvalidOperationException("Deliberate exception to verify 500 middleware.");
    }
}