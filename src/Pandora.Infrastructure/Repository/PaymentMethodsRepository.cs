using Pandora.Core.Contracts.Repositories;
using Pandora.Core.Entities;
using Pandora.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Infrastructure.Repository
{
    public class PaymentMethodsRepository : BaseRepository<PaymentMethod>, IPaymentMethodsRepository
    {
        public PaymentMethodsRepository(PandoraContext context) : base(context)
        {
        }
    }
}
