using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core.Entities
{
    public class Product : Entity
    {
        public string Name { get; private set; }
        public int Stock { get; private set; }
        public decimal Cost { get; private set; }

        public Product(string name, int stock, decimal cost)
        {
            SetNewId();
            Name = name;
            Stock = stock;
            Cost = cost;
        }

        public void Update(Product product)
        {
            Name = product.Name;
            Stock = product.Stock;
            Cost = product.Cost;
        }

        public void DecrementStock(int amount)
        {
            Stock -= amount;
        }

        public void IncrementStock(int amount)
        {
            Stock += amount;
        }
    }
}
