using Commerce.Core.DataTransferObjects;


namespace Commerce.Core.Services.Interfaces;

public interface IOrderService
{
    Task<List<OrderDTO>> GetAllOrders();
    Task<OrderDTO> GetOrderById(Guid id);
    Task InsertOrder(OrderDTO order);
    Task UpdateOrder(OrderDTO order, Guid id);
    Task DeleteOrder(Guid id);
}
