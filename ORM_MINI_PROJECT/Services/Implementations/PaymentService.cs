using ORM_MINI_PROJECT.DTOs;
using ORM_MINI_PROJECT.Excaption;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Repositories.Implementations;
using ORM_MINI_PROJECT.Repositories.Interfaces;
using ORM_MINI_PROJECT.Services.Interfances;

namespace ORM_MINI_PROJECT.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;

        public PaymentService()
        {
            _orderRepository = new OrderRepository();
            _paymentRepository = new PaymentRepository();
        }

        public async Task<List<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return payments.Select(p => new PaymentDto
            {
                Id = p.Id,
                OrderId = p.OrderId,
                Amount = p.Amount,
                
               
            }).ToList();
        }

        public async Task CreatePaymentAsync(PaymentDto newPayment)
        {
            var payment = new Payment
            {
                OrderId = newPayment.OrderId,
                Amount = newPayment.Amount,
               
                PaymentDate = DateTime.UtcNow
            };
            

            await _paymentRepository.CreateAsync(payment);
            await _paymentRepository.SaveChangesAsync();
        }

        public async Task UpdatePaymentAsync(PaymentDto updatedPayment)
        {
            var payment = await _paymentRepository.GetSingleAsync(p => p.Id == updatedPayment.Id);
            if (payment == null) throw new NotFoundException("Payment not found");

            payment.OrderId = updatedPayment.OrderId;
            payment.Amount = updatedPayment.Amount;

            payment.PaymentDate = DateTime.UtcNow;

            _paymentRepository.Update(payment);
            await _paymentRepository.SaveChangesAsync();
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await _paymentRepository.GetSingleAsync(p => p.Id == id);
            if (payment == null) throw new NotFoundException("Payment not found");

            _paymentRepository.Delete(payment);
            await _paymentRepository.SaveChangesAsync();
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(int id)
        {
            var payment = await _paymentRepository.GetSingleAsync(p => p.Id == id);
            if (payment == null) throw new NotFoundException("Payment not found");


            return new PaymentDto
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
               
                
            };

        }
    }
}
