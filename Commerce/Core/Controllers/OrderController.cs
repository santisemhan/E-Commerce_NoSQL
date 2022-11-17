using Commerce.Core.DataTransferObjects;
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
    public async Task<IActionResult> GetOrderById(string id)
    {
        return Ok(await orderService.GetOrderById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDTO order)
    {
        if (order == null) { return BadRequest(); }

        await orderService.InsertOrder(order);

        return Created("Created", true);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder([FromBody] OrderDTO order, string id)
    {
        if (order == null) { return BadRequest(); }

        await orderService.UpdateOrder(order, id);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(string id)
    {
        await orderService.DeleteOrder(id);
        return NoContent();
    }
}
