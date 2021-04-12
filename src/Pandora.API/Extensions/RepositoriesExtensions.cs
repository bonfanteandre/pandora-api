using Microsoft.Extensions.DependencyInjection;
using Pandora.Core.Contracts.Repositories;
using Pandora.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.API.Extensions
{
    public static class RepositoriesExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPlansRepository, PlansRepository>();
            services.AddTransient<IPaymentMethodsRepository, PaymentMethodsRepository>();
            services.AddTransient<IAddressesRepository, AddressesRepository>();
            services.AddTransient<ICustomersRepository, CustomersRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();
        }
    }
}
