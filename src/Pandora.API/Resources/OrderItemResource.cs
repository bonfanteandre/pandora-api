using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.API.Resources
{
    public class OrderItemResource
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal UnitValue { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
