using Cart.Core.DataTransferObjects;
using Commerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers() //ActionResult<List<UserDTO>>
    {
        return Ok(await userService.GetAllUsers());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        return Ok(await userService.GetUserById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
    {
        if (user == null) { return BadRequest(); }

        await userService.InsertUser(user);

        return Created("Created", true);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromBody] UserDTO user, string id)
    {
        if (user == null) { return BadRequest(); }

        await userService.UpdateUser(user, id);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await userService.DeleteUser(id);
        return NoContent();
    }
}
