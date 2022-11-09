namespace Cart.Core.Controllers
{
    using Cart.Core.DataTransferObjects;
    using Cart.Core.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class UserCartController : ControllerBase
    {
        private readonly IUserCartService _userCartService;

        public UserCartController(IUserCartService userCartService)
        {
            _userCartService = userCartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserCart([FromQuery] Guid userId)
        {
            var result = await _userCartService.GetUserCartAsync(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserCart([FromBody] UserCartDTO userCartInfo)
        {
            await _userCartService.ChangeUserCart(userCartInfo);
            return NoContent();
        }

        [HttpPost]
        [Route("checkout")]
        public async Task<IActionResult> Checkout([FromBody] UserCartDTO checkoutInfo)
        {
            return NoContent();
        }
    }
}
