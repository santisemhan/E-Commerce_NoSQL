using Commerce.Core.DataTransferObjects;

namespace Commerce.Core.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<List<PaymentDTO>> GetAllPayments();
        Task<PaymentDTO> GetPaymentById(string id);
        Task InsertPayment(string orderId, string userId, string paymentType);
    }
}
