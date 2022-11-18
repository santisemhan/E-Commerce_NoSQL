using Commerce.Core.DataTransferObjects;
using Commerce.Core.Exceptions;
using Commerce.Core.Repositories.Interfaces;
using Commerce.Core.Services.Interfaces;
using System.Net;

namespace Commerce.Core.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IUserService userService;
    private readonly IOrderService orderService;

    public PaymentService(IPaymentRepository paymentRepository, IUserService userService, IOrderService orderService)
    {
        this.paymentRepository = paymentRepository;
        this.userService = userService;
        this.orderService = orderService;
    }

    public async Task<List<PaymentDTO>> GetAllPayments()
    {
        return await paymentRepository.GetAll();
    }

    public async Task<PaymentDTO> GetPaymentById(Guid id)
    {
        var payment = await paymentRepository.GetById(id);
        if(payment is null)
        {
            throw new AppException("El pago no existe", HttpStatusCode.NotFound);
        }
        return payment;
    }
    public async Task InsertPayment(Guid orderId, Guid userId,string paymentType)
    {
        await orderService.GetOrderById(orderId); //existe la orden?

        
        var user = await userService.GetUserById(userId);

        PaymentDTO newPayment = new()
        {
            OrderId = orderId,
            User = user,
            TimeStamp = DateTime.UtcNow,
            PaymentType = paymentType
        };

        await paymentRepository.Insert(newPayment);
    }
}
