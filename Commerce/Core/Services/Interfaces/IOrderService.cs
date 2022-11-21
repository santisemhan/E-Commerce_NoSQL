using Commerce.Core.DataTransferObjects;
using Commerce.Core.DataTransferObjects.Request;
using Commerce.Core.Models;

namespace Commerce.Core.Services.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetAllOrders();

    Task<Order> GetOrderById(Guid id);

    Task InsertOrder(OrderDTO order);

    Task DeleteOrder(Guid id);

    Task<List<Order>> GetOrderByStatus(bool status);

    Task ChangeStatus(Order order);
}
