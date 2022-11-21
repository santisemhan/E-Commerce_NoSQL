using Commerce.Core.DataTransferObjects;
using Commerce.Core.DataTransferObjects.Request;
using Commerce.Core.Models;
using Commerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(await productService.GetAllProducts());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById([FromRoute] Guid id)
    {
        return Ok(await productService.GetProductById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDTO product)
    {
        if (product == null) { return BadRequest(); }

        await productService.InsertProduct(product);

        return Created("Created", true);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
        if (product == null) { return BadRequest(); }

        await productService.UpdateProduct(product);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
    {
        await productService.DeleteProduct(id);
        return NoContent();
    }
}