using Commerce.Core.DataTransferObjects;
using Commerce.Core.Exceptions;
using Commerce.Core.Repositories.Interfaces;
using Commerce.Core.Services.Interfaces;
using System.Net;

namespace Commerce.Core.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository orderRepository;
    public OrderService(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }
    public async Task DeleteOrder(Guid id)
    {
        await orderRepository.Delete(id);
    }

    public async Task<List<OrderDTO>> GetAllOrders()
    {
        return await orderRepository.GetAll();
    }

    public async Task<OrderDTO> GetOrderById(Guid id)
    {
        var order = await orderRepository.GetById(id);
        if (order is null)
        {
            throw new AppException("El pedido no existe", HttpStatusCode.NotFound);
        }
        return order;
    }

    public async Task InsertOrder(OrderDTO order)
    {
        await orderRepository.Insert(order);
    }

    public async Task UpdateOrder(OrderDTO order, Guid id)
    {
        await GetOrderById(id);
        order.OrderId = id;
        await orderRepository.Update(order);
    }
}
