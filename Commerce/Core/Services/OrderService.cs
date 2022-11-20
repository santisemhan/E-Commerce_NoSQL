﻿using Commerce.Core.DataTransferObjects.Request;
using Commerce.Core.Exceptions;
using Commerce.Core.Models;
using Commerce.Core.Repositories.Interfaces;
using Commerce.Core.Services.Interfaces;
using System.Net;

namespace Commerce.Core.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository orderRepository;
    private readonly IProductRepository productrRepository;
    private readonly IUserService userService;
    private readonly ICatalogService catalogService;

    public OrderService(IOrderRepository orderRepository, ICatalogService catalogService, IProductRepository productrRepository, IUserService userService)
    {
        this.orderRepository = orderRepository;
        this.productrRepository = productrRepository;
        this.userService = userService;
        this.catalogService = catalogService;
    }
    public async Task DeleteOrder(Guid id)
    {
        await orderRepository.Delete(id);
    }

    public async Task<List<Order>> GetAllOrders()
    {
        return await orderRepository.GetAll();
    }

    public async Task<Order> GetOrderById(Guid id)
    {
        var order = await orderRepository.GetById(id);
        if (order is null)
        {
            throw new AppException("El pedido no existe", HttpStatusCode.NotFound);
        }
        return order;
    }

    public async Task<List<Order>> GetOrderByStatus(bool status)
    {
        return await orderRepository.GetAllByStatus(status);
    }

    public async Task ChangeStatus(Order order)
    {
        await orderRepository.Update(order);
    }

    public async Task InsertOrder(OrderDTO order)
    {
        User user = await userService.GetUserById(order.idUser);
        var FinalPriceAux = 0;
        List<ProductCart> ProductsCart = new List<ProductCart>();
        foreach (ProductCartDTO product in order.Products)
        {
            ProductCatalog productoCatalog = await catalogService.GetCatalogById(product.ProductCatalogId);
            Product producto = await productrRepository.GetById(productoCatalog.ProductId);
            if (producto.Stock < product.Quantity)
            {
                throw new AppException("No hay stock suficiente", HttpStatusCode.NotFound);
            }
        }
        foreach (ProductCartDTO product in order.Products)
        {
            ProductCatalog productoCatalog = await catalogService.GetCatalogById(product.ProductCatalogId);
            Product producto = await productrRepository.GetById(productoCatalog.ProductId);
            producto.Stock -= product.Quantity;
            await productrRepository.Update(producto);

            // agregamos los productos
            ProductsCart.Add(new ProductCart () { ProductCatalog = productoCatalog,Quantity = product.Quantity });

            //calculamos el precio
            FinalPriceAux += (int)(productoCatalog.Price * product.Quantity);
        }

        Order neworder = new()
        {
            TimeStamp = DateTime.Now,
            User = user,
            Products = ProductsCart,
            IVA = order.IVA,
            FinalPrice = FinalPriceAux
        };

        await orderRepository.Insert(neworder);
    }
}
