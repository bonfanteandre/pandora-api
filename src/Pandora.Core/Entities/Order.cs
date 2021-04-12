﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core.Entities
{
    public class Order : Entity
    {
        public OrderStatus Status { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public Guid PaymentMethodId { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public DateTime DeliverAt { get; private set; }
        public string Observations { get; private set; }
        public decimal Total { get; private set; }
        public ICollection<OrderItem> Items { get; private set; }

        public Order()
        {
            SetNewId();
            Items = new List<OrderItem>();
            Status = OrderStatus.Created;
        }

        public void Update(Order order)
        {
            CustomerId = order.CustomerId;
            PaymentMethodId = order.PaymentMethodId;
            DeliverAt = order.DeliverAt;
            Observations = order.Observations;
        }

        public void Finish()
        {
            Status = OrderStatus.Finished;
        }

        public void Deliver()
        {
            Status = OrderStatus.Delivered;
        }

        public void Cancel()
        {
            Status = OrderStatus.Canceled;
        }
    }
}
