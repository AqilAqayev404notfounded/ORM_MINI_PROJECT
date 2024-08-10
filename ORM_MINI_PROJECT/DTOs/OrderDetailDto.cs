using ORM_MINI_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MINI_PROJECT.DTOs;

public class OrderDetailDto
{
    public int Id { get; set; }
    public int OrderID { get; set; }
    public Order Order { get; set; }

    public int ProductID { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }

    public decimal PricePerItem { get; set; }
}
