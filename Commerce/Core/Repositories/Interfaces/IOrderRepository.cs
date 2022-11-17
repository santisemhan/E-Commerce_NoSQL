using Commerce.Core.DataTransferObjects;

namespace Commerce.Core.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<List<OrderDTO>> GetAll();
    Task<OrderDTO> GetById(string id);
    Task Insert(OrderDTO order);
    Task Update(OrderDTO order);
    Task Delete(string order);
}
