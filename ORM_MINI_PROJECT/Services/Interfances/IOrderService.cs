using ORM_MINI_PROJECT.DTOs;

namespace ORM_MINI_PROJECT.Services.Interfances
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrdersAsync(int userId);
        Task CreateOrderAsync(OrderDto newOrder,int userid);
        Task UpdateOrderAsync(OrderDto updatedOrder, int userId);
        Task DeleteOrderAsync(int id, int userId);
        Task<OrderDto> GetOrderByIdAsync(int id,int userId);
    }
}
