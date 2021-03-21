using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core.Entities
{
    public class Plan : Entity
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Plan(string name, decimal price)
        {
            SetNewId();
            Name = name;
            Price = price;
        }

        public void Update(Plan plan)
        {
            Name = plan.Name;
            Price = plan.Price;
        }
    }
}
