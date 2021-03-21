using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pandora.API.Resources;
using Pandora.Core.Contracts.Services;
using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/plans")]
    public class PlansController : ControllerBase
    {
        private readonly IPlansService _plansService;
        private readonly IMapper _mapper;

        public PlansController(IPlansService plansService, IMapper mapper)
        {
            _plansService = plansService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlanResource planResource)
        {
            var plan = _mapper.Map<PlanResource, Plan>(planResource);
            var result = await _plansService.AddAsync(plan);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut, Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] PlanResource planResource)
        {
            var plan = _mapper.Map<PlanResource, Plan>(planResource);
            var result = await _plansService.UpdateAsync(id, plan);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete, Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _plansService.RemoveAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> FilterAndPage(
            [FromQuery] string name,
            [FromQuery] int skip = 0, 
            [FromQuery] int take = 10)
        {
            var plans = await _plansService.FilterAndPaged(skip, take, name);

            if (plans == null || plans.Count == 0)
            {
                return NoContent();
            }

            return Ok(plans);
        }

        [HttpGet, Route("{id:guid}")]
        public async Task<IActionResult> Find([FromRoute] Guid id)
        {
            var plan = await _plansService.FindAsync(id);

            if (plan == null)
            {
                return NoContent();
            }

            return Ok(plan);
        }
    }
}
