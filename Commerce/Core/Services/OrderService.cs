using Commerce.Core.DataTransferObjects;
using Commerce.Core.Exceptions;
using Commerce.Core.Repositories.Interfaces;
using Commerce.Core.Services.Interfaces;
using System.Net;

namespace Commerce.Core.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository orderRepository;
    private readonly IProductRepository productrRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productrRepository)
    {
        this.orderRepository = orderRepository;
        this.productrRepository = productrRepository;
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
        foreach(ProductCartDTO product in order.Products)
        {
            var producto = await productrRepository.GetById(product.ProductCatalog.ProductId);
            if (producto is null)
            {
                throw new AppException("Hay productos que no existen", HttpStatusCode.NotFound);
            }
            if (producto.Stock < product.Quantity)
            {
                throw new AppException("No hay stock suficiente", HttpStatusCode.NotFound);
            }
        }
        foreach (ProductCartDTO product in order.Products)
        {
            var producto = await productrRepository.GetById(product.ProductCatalog.ProductId);
            producto.Stock = producto.Stock - product.Quantity;
            await productrRepository.Update(producto);
        }
        await orderRepository.Insert(order);
    }

    public async Task UpdateOrder(OrderDTO order, Guid id)
    {
        foreach (ProductCartDTO product in order.Products)
        {
            var producto = await productrRepository.GetById(product.ProductCatalog.ProductId);
            if (producto is null)
            {
                throw new AppException("Hay productos que no existen", HttpStatusCode.NotFound);
            }
            if (producto.Stock < product.Quantity)
            {
                throw new AppException("No hay stock suficiente", HttpStatusCode.NotFound);
            }
        }
        var originalOrder = await GetOrderById(id);
        order.OrderId = id;
        foreach (ProductCartDTO product in order.Products)
        {
            var originalProduct = new ProductCartDTO();
            foreach (ProductCartDTO p in originalOrder.Products)
            {
                if (p.ProductCatalog.ProductId == product.ProductCatalog.ProductId)
                {
                    originalProduct = p;
                }
            }

            var producto = await productrRepository.GetById(product.ProductCatalog.ProductId);
            producto.Stock = producto.Stock - (product.Quantity - originalProduct.Quantity);
            await productrRepository.Update(producto);
        }
        await orderRepository.Update(order);
    }
}
