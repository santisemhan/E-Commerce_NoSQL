using Commerce.Core.Models;

namespace Commerce.Core.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetAll();

    Task<Order> GetById(Guid id);

    Task Insert(Order order);

    Task Update(Order order);

    Task Delete(Guid order);
    Task<List<Order>> GetAllByStatus(bool status);
}
