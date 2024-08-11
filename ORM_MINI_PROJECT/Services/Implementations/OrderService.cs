using ORM_MINI_PROJECT.DTOs;
using ORM_MINI_PROJECT.Excaption;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Repositories.Interfaces;
using ORM_MINI_PROJECT.Services.Interfances;

namespace ORM_MINI_PROJECT.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Select(o => new OrderDto
        {
            Id = o.id,
            UserId = o.UserId,
            TotalAmount = o.TotalAmount,
            Status = o.Status,
        }).ToList();
    }

    public async Task<OrderDto> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetSingleAsync(o => o.id == id);
        if (order == null) throw new NotFoundException("Order not found");

        return new OrderDto
        {
            Id = order.id,
            UserId = order.UserId,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
        };
    }

    public async Task CreateOrderAsync(OrderDto newOrder)
    {
        var order = new Order
        {
            UserId = newOrder.UserId,
            TotalAmount = newOrder.TotalAmount,
            Status = newOrder.Status,
            OrderDate = DateTime.UtcNow
        };

        await _orderRepository.CreateAsync(order);
        await _orderRepository.SaveChangesAsync();
    }

    public async Task UpdateOrderAsync(OrderDto updatedOrder)
    {
        var order = await _orderRepository.GetSingleAsync(o => o.id == updatedOrder.Id);
        if (order == null) throw new NotFoundException("Order not found");

        order.TotalAmount = updatedOrder.TotalAmount;
        order.Status = updatedOrder.Status;
        order.OrderDate = DateTime.UtcNow;

        _orderRepository.Update(order);
        await _orderRepository.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _orderRepository.GetSingleAsync(o => o.id == id);
        if (order == null) throw new NotFoundException("Order not found");

        _orderRepository.Delete(order);
        await _orderRepository.SaveChangesAsync();
    }
}
