using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.API.Resources
{
    public class OrderResource
    {
        public Guid CustomerId { get; set; }
        public Guid AddressId { get; set; }
        public Guid PaymentMethodId { get; set; }
        public DateTime DeliverAt { get; set; }
        public string Observations { get; set; }
        public decimal Total { get; set; }
    }
}
