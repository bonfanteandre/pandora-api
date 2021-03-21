using AutoMapper;
using Pandora.API.Resources;
using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.API.Mapping
{
    public class ResourceToEntityProfile : Profile
    {
        public ResourceToEntityProfile()
        {
            CreateMap<PlanResource, Plan>();
        }
    }
}
