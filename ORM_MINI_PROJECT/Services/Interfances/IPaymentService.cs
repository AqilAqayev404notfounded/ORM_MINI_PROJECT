
using ORM_MINI_PROJECT.DTOs;

namespace ORM_MINI_PROJECT.Services.Interfances;

public interface IPaymentService
    {
        Task<List<PaymentDto>> GetAllPaymentsAsync();
        Task CreatePaymentAsync(PaymentDto newPayment);
        Task UpdatePaymentAsync(PaymentDto updatedPayment);
        Task DeletePaymentAsync(int id);
        Task<PaymentDto> GetPaymentByIdAsync(int id);
    }




