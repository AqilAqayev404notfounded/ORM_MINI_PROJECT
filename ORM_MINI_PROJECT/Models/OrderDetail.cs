using ORM_MINI_PROJECT.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MINI_PROJECT.Models;

public class OrderDetail : BaseEntity
{
    public int OrderID { get; set; }
    public Order Order { get; set; }

    public int ProductID { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }

    public decimal PricePerItem { get; set; }

    public List<OrderDetail> OrderDetails {  get; set; }


}
