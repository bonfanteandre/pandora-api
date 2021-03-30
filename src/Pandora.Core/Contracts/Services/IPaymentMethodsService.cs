using Pandora.Core.Entities;
using Pandora.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pandora.Core.Contracts.Services
{
    public interface IPaymentMethodsService : IService<PaymentMethod>
    {
        Task<ICollection<PaymentMethod>> FilterAndPaged(int skip, int take, string name);
    } 
}