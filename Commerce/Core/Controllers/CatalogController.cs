using Commerce.Core.DataTransferObjects;
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
    public async Task<IActionResult> GetAllCatalogs()
    {
        return Ok(await catalogService.GetAllCatalogs());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCatalogById(Guid id)
    {
        return Ok(await catalogService.GetCatalogById(id));
    }

    [HttpGet("/log/{id}")]
    public async Task<IActionResult> GetCatalogLogById(Guid id)
    {
        return Ok(await catalogService.GetCatalogLogById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCatalog([FromBody] ProductCatalogDTO catalog)
    {
        if (catalog == null) { return BadRequest(); }

        await catalogService.InsertCatalog(catalog);

        return Created("Created", true);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCatalog([FromBody] ProductCatalogDTO catalog, Guid id)
    {
        if (catalog == null) { return BadRequest(); }

        await catalogService.UpdateCatalog(catalog, id);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCatalog(Guid id)
    {
        await catalogService.DeleteCatalog(id);
        return NoContent();
    }
}
