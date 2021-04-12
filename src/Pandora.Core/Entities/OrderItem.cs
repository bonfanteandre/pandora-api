using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core.Entities
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public decimal UnitValue { get; private set; }
        public int Amount { get; private set; }
        public decimal Price { get; private set; }

        public OrderItem()
        {
            SetNewId();
        }
    }
}
