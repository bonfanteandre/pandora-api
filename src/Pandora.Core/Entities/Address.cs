using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core.Entities
{
    public class Address : Entity
    {
        public Guid CustomerId { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string Reference { get; private set; }

        public Address(
            Guid customerId, 
            string street, 
            string number, 
            string neighborhood, 
            string reference)
        {
            SetNewId();
            CustomerId = customerId;
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            Reference = reference;
        }

        public void Update(Address address)
        {
            Street = address.Street;
            Number = address.Number;
            Neighborhood = address.Neighborhood;
            Reference = address.Reference;
        }
    }
}
