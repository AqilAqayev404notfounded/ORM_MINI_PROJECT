using ORM_MINI_PROJECT.DTOs;
using ORM_MINI_PROJECT.Excaption;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Repositories.Implementations;
using ORM_MINI_PROJECT.Repositories.Interfaces;
using ORM_MINI_PROJECT.Services.Interfances;

namespace ORM_MINI_PROJECT.Services.Implementations
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderDetailService()
        {
            _orderDetailRepository = new OrderDetailRepository();
            _orderRepository = new OrderRepository();
            _productRepository = new ProductRepository();
        }

        public async Task<List<OrderDetailDto>> GetAllOrderDetailsAsync()
        {
            var orderDetails = await _orderDetailRepository.GetAllAsync();
            return orderDetails.Select(od => new OrderDetailDto
            {
                Id = od.Id,
                OrderID = od.OrderId,
                ProductID = od.ProductId,
                Quantity = od.Quantity,
                PricePerItem = od.PricePerItem
            }).ToList();
        }

        public async Task<OrderDetailDto> GetOrderDetailByIdAsync(int id)
        {
            var orderDetail = await _orderDetailRepository.GetSingleAsync(od => od.Id == id);
            if (orderDetail == null) throw new NotFoundException("Order detail not found");

            return new OrderDetailDto
            {
                Id = orderDetail.Id,
                OrderID = orderDetail.OrderId,
                ProductID = orderDetail.ProductId,
                Quantity = orderDetail.Quantity,
                PricePerItem = orderDetail.PricePerItem
            };
        }

        public async Task CreateOrderDetailAsync(OrderDetailDto newOrderDetail)
        {
            //var order = await _orderRepository.GetSingleAsync(x => x.Id == newOrderDetail.OrderID, "OrderDetails.Product");

            //if (order is null)
            //    throw new NotFoundException("Order is not found");

            //var product = await _productRepository.GetSingleAsync(x => x.Id == newOrderDetail.ProductID);

            //if (product is null)
            //    throw new NotFoundException("Product is not found");


            //var orderDetail = new OrderDetail
            //{
            //    OrderId = newOrderDetail.OrderID,
            //    ProductId = newOrderDetail.ProductID,
            //    Quantity = newOrderDetail.Quantity,
            //    PricePerItem = product.Price
            //};


            //order.TotalAmount = 0;
            //order.OrderDetails.ForEach(x => order.TotalAmount += x.Quantity * x.Product.Price);
            //order.TotalAmount += orderDetail.Quantity * product.Price;
            //order.OrderDetails.Add(orderDetail);



            // _orderRepository.Update(order);

            //await _orderDetailRepository.CreateAsync(orderDetail);
            //await _orderDetailRepository.SaveChangesAsync();







            var order = await _orderRepository.GetSingleAsync(x => x.Id == newOrderDetail.OrderID, "OrderDetails.Product");
         
         


            if (order is null)
                throw new NotFoundException("Order is not found");


            var product = await _productRepository.GetSingleAsync(x => x.Id == newOrderDetail.ProductID);

            if (product is null)
                throw new NotFoundException("Product is not found");


            order.TotalAmount += product.Price * newOrderDetail.Quantity;

            OrderDetail orderDetail = new()
            {
                OrderId = order.Id,
                PricePerItem = product.Price,
                ProductId = product.Id,
                Quantity = newOrderDetail.Quantity,

            };


            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();

            await _orderDetailRepository.CreateAsync(orderDetail);
            await _orderDetailRepository.SaveChangesAsync();
        }

        public async Task UpdateOrderDetailAsync(OrderDetailDto updatedOrderDetail)
        {
            var orderDetail = await _orderDetailRepository.GetSingleAsync(od => od.Id == updatedOrderDetail.Id);
            if (orderDetail == null) throw new NotFoundException("Order detail not found");

            orderDetail.Quantity = updatedOrderDetail.Quantity;
            orderDetail.PricePerItem = updatedOrderDetail.PricePerItem;

            _orderDetailRepository.Update(orderDetail);
            await _orderDetailRepository.SaveChangesAsync();
        }

        public async Task DeleteOrderDetailAsync(int id)
        {
            var orderDetail = await _orderDetailRepository.GetSingleAsync(od => od.Id == id);
            if (orderDetail == null) throw new NotFoundException("Order detail not found");

            _orderDetailRepository.Delete(orderDetail);
            await _orderDetailRepository.SaveChangesAsync();
        }
    }
}
