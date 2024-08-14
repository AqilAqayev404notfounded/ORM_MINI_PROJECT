﻿using ORM_MINI_PROJECT.DTOs;
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

        public OrderDetailService( )
        {
            _orderDetailRepository = new OrderDetailRepository();
        }

        public async Task<List<OrderDetailDto>> GetAllOrderDetailsAsync()
        {
            var orderDetails = await _orderDetailRepository.GetAllAsync();
            return orderDetails.Select(od => new OrderDetailDto
            {
                Id = od.Id,
                OrderID = od.OrderID,
                ProductID = od.ProductID,
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
                OrderID = orderDetail.OrderID,
                ProductID = orderDetail.ProductID,
                Quantity = orderDetail.Quantity,
                PricePerItem = orderDetail.PricePerItem
            };
        }

        public async Task CreateOrderDetailAsync(OrderDetailDto newOrderDetail)
        {
            var orderDetail = new OrderDetail
            {
                OrderID = newOrderDetail.OrderID,
                ProductID = newOrderDetail.ProductID,
                Quantity = newOrderDetail.Quantity,
                PricePerItem = newOrderDetail.PricePerItem
            };

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
