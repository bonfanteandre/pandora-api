using Microsoft.EntityFrameworkCore;
using Pandora.Core.Contracts.Repositories;
using Pandora.Core.Entities;
using Pandora.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(PandoraContext context) : base(context)
        {
        }
    }
}
