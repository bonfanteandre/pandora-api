using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.API.Resources
{
    public class ProductResource
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Cost { get; set; }
    }
}
