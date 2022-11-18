using Commerce.Core.DataTransferObjects;

namespace Commerce.Core.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<List<PaymentDTO>> GetAllPayments();
        Task<PaymentDTO> GetPaymentById(Guid id);
        Task InsertPayment(Guid orderId, Guid userId, string paymentType);
    }
}
