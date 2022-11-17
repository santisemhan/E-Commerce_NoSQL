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
    public async Task DeleteOrder(string id)
    {
        await orderRepository.Delete(id);
    }

    public async Task<List<OrderDTO>> GetAllOrders()
    {
        return await orderRepository.GetAll();
    }

    public async Task<OrderDTO> GetOrderById(string id)
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

    public async Task UpdateOrder(OrderDTO order, string id)
    {
        await GetOrderById(id);
        order.OrderId = new MongoDB.Bson.ObjectId(id);
        await orderRepository.Update(order);
    }
}
