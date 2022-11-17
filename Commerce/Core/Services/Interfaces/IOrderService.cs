using Commerce.Core.DataTransferObjects;


namespace Commerce.Core.Services.Interfaces;

public interface IOrderService
{
    Task<List<OrderDTO>> GetAllOrders();
    Task<OrderDTO> GetOrderById(string id);
    Task InsertOrder(OrderDTO order);
    Task UpdateOrder(OrderDTO order, string id);
    Task DeleteOrder(string id);
}
