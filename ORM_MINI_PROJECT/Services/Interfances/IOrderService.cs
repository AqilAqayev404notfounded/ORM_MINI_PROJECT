using ORM_MINI_PROJECT.DTOs;

namespace ORM_MINI_PROJECT.Services.Interfances
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrdersAsync();
        Task CreateOrderAsync(OrderDto newOrder);
        Task UpdateOrderAsync(OrderDto updatedOrder);
        Task DeleteOrderAsync(int id);
        Task<OrderDto> GetOrderByIdAsync(int id);
    }
}
