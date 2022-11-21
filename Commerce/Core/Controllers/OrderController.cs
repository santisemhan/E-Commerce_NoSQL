using Commerce.Core.DataTransferObjects;
using Commerce.Core.DataTransferObjects.Request;
using Commerce.Core.Models;
using Commerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService orderService;

    public OrderController(IOrderService orderService)
    {
        this.orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        return Ok(await orderService.GetAllOrders());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById([FromRoute] Guid id)
    {
        return Ok(await orderService.GetOrderById(id));
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetOrderByStatus([FromQuery] bool status)
    {
        return Ok(await orderService.GetOrderByStatus(status));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDTO order)
    {
        if (order == null) { return BadRequest(); }

        await orderService.InsertOrder(order);

        return Created("Created", true);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
    {
        await orderService.DeleteOrder(id);
        return NoContent();
    }
}
