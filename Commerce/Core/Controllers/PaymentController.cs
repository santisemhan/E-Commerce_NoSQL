using Commerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            return Ok(await paymentService.GetAllPayments());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById([FromRoute] Guid id)
        {
            
            return Ok(await paymentService.GetPaymentById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromQuery] Guid orderId, [FromQuery] Guid userId, [FromQuery] string paymentType)
        {
            await paymentService.InsertPayment(orderId, userId, paymentType);

            return Created("Created", true);
        }
    }
}
