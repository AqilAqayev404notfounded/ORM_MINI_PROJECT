using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Repositories.Implementations.Generic;
using ORM_MINI_PROJECT.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MINI_PROJECT.Repositories.Implementations
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
    }
}
