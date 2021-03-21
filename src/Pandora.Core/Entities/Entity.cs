using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        protected void SetNewId()
        {
            Id = Guid.NewGuid();
        }
    }
}
