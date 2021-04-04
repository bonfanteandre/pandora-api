using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.API.Resources
{
    public class AddressResource
    {
        public Guid CustomerId { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string Reference { get; set; }
    }
}
