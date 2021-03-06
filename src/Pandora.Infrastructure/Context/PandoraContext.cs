using Microsoft.EntityFrameworkCore;
using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Infrastructure.Context
{
    public class PandoraContext : DbContext
    {
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public PandoraContext(DbContextOptions options) : base(options)
        {
        }
    }
}
