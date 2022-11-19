using Commerce.Core.Models;

namespace Commerce.Core.Repositories.Interfaces;

public interface IPaymentRepository
{
    Task<List<Payment>> GetAll();

    Task<Payment> GetById(Guid id);

    Task Insert(Payment newPayment);
}
