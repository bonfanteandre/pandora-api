using Pandora.Core.Contracts.Repositories;
using Pandora.Core.Entities;
using Pandora.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Infrastructure.Repository
{
    public class AddressesRepository : IAddressesRepository
    {
        private readonly PandoraContext _context;

        public AddressesRepository(PandoraContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Address address)
        {
            await _context.Addresses.AddAsync(address);
        }

        public Task UpdateAsync(Address address)
        {
            _context.Addresses.Update(address);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Address address)
        {
            _context.Addresses.Remove(address);
            return Task.CompletedTask;
        }

        public async Task<Address> FindAsync(Guid id)
        {
            return await _context.Addresses.FindAsync(id);
        }
    }
}
