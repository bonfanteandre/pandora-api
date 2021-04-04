using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core.Entities
{
    public class Customer : Entity
    {
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public Guid PlanId { get; private set; }
        public Plan Plan { get; private set; }
        public ICollection<Address> Addresses { get; private set; }

        public Customer()
        {
            SetNewId();
            Addresses = new List<Address>();
        }

        public Customer(string name, string phoneNumber, Guid planId)
        {
            SetNewId();
            Name = name;
            PhoneNumber = phoneNumber;
            PlanId = planId;
        }

        public void Update(Customer customer)
        {
            Name = customer.Name;
            PhoneNumber = customer.PhoneNumber;
            PlanId = customer.PlanId;
        }
    }
}
