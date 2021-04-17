using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Pandora.Core.Entities
{
    public enum OrderStatus
    {
        [Description("Criado")]
        Created,
        [Description("Finalizado")]
        Finished,
        [Description("Entregue")]
        Delivered,
        [Description("Cancelado")]
        Canceled
    }
}
