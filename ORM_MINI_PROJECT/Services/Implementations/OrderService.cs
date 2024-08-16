using DocumentFormat.OpenXml.Spreadsheet;
using ORM_MINI_PROJECT.DTOs;
using ORM_MINI_PROJECT.Excaption;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Repositories.Implementations;
using ORM_MINI_PROJECT.Repositories.Interfaces;
using ORM_MINI_PROJECT.Services.Interfances;

namespace ORM_MINI_PROJECT.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService()
    {
        _orderRepository = new OrderRepository();
    }

    public async Task<List<OrderDto>> GetAllOrdersAsync(int userId)
    {
        var order = await _orderRepository.GetAllAsync();
        var orderdto = new List<OrderDto>();
        foreach (var item in order)
        {
            if (item.Id == userId)
            {

                var orders = await _orderRepository.GetAllAsync();
                 orders.Select(o => new OrderDto
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                }).ToList();
                var orderDto = new OrderDto { Id = item.Id,UserId=item.Id, TotalAmount = item.TotalAmount, Status = item.Status };
                orderdto.Add(orderDto);
            }
        }
        return orderdto;

    }

    public async Task<OrderDto> GetOrderByIdAsync(int id, int userId)
    {
        var order = await _orderRepository.GetSingleAsync(o => o.Id == id && o.UserId == userId);
        if (order == null) throw new NotFoundException("Order not found");

        return new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
        };
    }

    public async Task CreateOrderAsync(OrderDto newOrder, int userId)
    {

        var isExist = await _orderRepository.IsExistAsync(x => x.UserId == newOrder.UserId && x.Status == Enums.OrderStatus.pending && x.UserId == userId);

        if (isExist)
            throw new Exception("Order is already exist exception");


        var order = new Order
        {
            UserId = newOrder.UserId,
            Status = Enums.OrderStatus.pending,
            OrderDate = DateTime.UtcNow
        };

        await _orderRepository.CreateAsync(order);
        await _orderRepository.SaveChangesAsync();


        await Console.Out.WriteLineAsync($"Order successfully created this your order id {order.Id}");
    }

    public async Task UpdateOrderAsync(OrderDto updatedOrder, int userId)
    {
        var order = await _orderRepository.GetSingleAsync(o => o.Id == updatedOrder.Id && o.UserId == userId);
        if (order == null) throw new NotFoundException("Order not found");

        order.Status = updatedOrder.Status;
        order.OrderDate = DateTime.UtcNow;
        await _orderRepository.SaveChangesAsync();

        _orderRepository.Update(order);
        await _orderRepository.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(int id, int userId)
    {
        var order = await _orderRepository.GetSingleAsync(o => o.Id == id && o.UserId == userId);
        if (order == null) throw new NotFoundException("Order not found");

        _orderRepository.Delete(order);
        await _orderRepository.SaveChangesAsync();
    }

}
