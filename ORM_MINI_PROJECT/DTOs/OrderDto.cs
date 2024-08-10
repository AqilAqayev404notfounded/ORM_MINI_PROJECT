using ORM_MINI_PROJECT.Enums;
using ORM_MINI_PROJECT.Models;

namespace ORM_MINI_PROJECT.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
    }
}
