﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Core.Contracts.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
