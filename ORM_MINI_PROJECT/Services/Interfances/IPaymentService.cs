
using ORM_MINI_PROJECT.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MINI_PROJECT.Services.Interfances
{
    public interface IPaymentService
    {using ORM_MINI_PROJECT.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORM_MINI_PROJECT.Services.Interfances
    {
        public interface IPaymentService
        {
            Task<List<PaymentDto>> GetAllPaymentsAsync();
            Task CreatePaymentAsync(PaymentDto newPayment);
            Task UpdatePaymentAsync(PaymentDto updatedPayment);
            Task DeletePaymentAsync(int id);
            Task<PaymentDto> GetPaymentByIdAsync(int id);
        }
    }

}
}
