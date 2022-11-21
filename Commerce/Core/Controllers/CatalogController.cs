using Commerce.Core.DataTransferObjects;
using Commerce.Core.DataTransferObjects.Request;
using Commerce.Core.Models;
using Commerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        this.catalogService = catalogService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCatalog()
    {
        return Ok(await catalogService.GetAllProductsCatalog());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductCatalogById([FromRoute] Guid id)
    {
        return Ok(await catalogService.GetProductCatalogById(id));
    }

    [HttpGet("log/{id}")]
    public async Task<IActionResult> GetProductCatalogLogById([FromRoute] Guid id)
    {
        return Ok(await catalogService.GetProductCatalogLogById(id));
    }

    [HttpPost]
    public async Task<IActionResult> InsertProductCatalog([FromBody] ProductCatalogDTO catalog)
    {
        if (catalog == null) { return BadRequest(); }

        await catalogService.InsertProductCatalog(catalog);

        return Created("Created", true);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductCatalog([FromBody] ProductCatalog catalog)
    {
        if (catalog == null) { return BadRequest(); }

        await catalogService.UpdateProductCatalog(catalog);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductCatalog([FromRoute] Guid id)
    {
        await catalogService.DeleteProductCatalog(id);
        return NoContent();
    }
}
