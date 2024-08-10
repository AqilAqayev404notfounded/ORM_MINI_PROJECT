using ORM_MINI_PROJECT.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORM_MINI_PROJECT.Services.Interfances
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetailDto>> GetAllOrderDetailsAsync();
        Task CreateOrderDetailAsync(OrderDetailDto newOrderDetail);
        Task UpdateOrderDetailAsync(OrderDetailDto updatedOrderDetail);
        Task DeleteOrderDetailAsync(int id);
        Task<OrderDetailDto> GetOrderDetailByIdAsync(int id);
    }
}
