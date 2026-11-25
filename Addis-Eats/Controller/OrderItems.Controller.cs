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
            var error = result.Error;
            if (error is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    code = "SERVER.UNEXPECTED_ERROR",
                    message = "An unexpected error occurred."
                });
            }

            return StatusCode(400, new
            {
                code = 400,
                message = "Whoops !"
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