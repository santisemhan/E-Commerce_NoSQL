using Commerce.Core.DataTransferObjects;

namespace Commerce.Core.Repositories.Interfaces;

public interface IPaymentRepository
{
    Task<List<PaymentDTO>> GetAll();
    Task<PaymentDTO> GetById(Guid id);
    Task Insert(PaymentDTO newPayment);
}
