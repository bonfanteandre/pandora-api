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
    [Route("api/v{version:apiVersion}/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _customersService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomersService customersService, IMapper mapper)
        {
            _customersService = customersService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerResource customerResource)
        {
            if (customerResource == null)
            {
                customerResource = new CustomerResource();
            }

            var customer = _mapper.Map<CustomerResource, Customer>(customerResource);
            var result = await _customersService.AddAsync(customer);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut, Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CustomerResource customerResource)
        {
            if (customerResource == null)
            {
                customerResource = new CustomerResource();
            }

            var customer = _mapper.Map<CustomerResource, Customer>(customerResource);
            var result = await _customersService.UpdateAsync(id, customer);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete, Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _customersService.RemoveAsync(id);

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
            var customers = await _customersService.FilterAndPaged(skip, take, name);

            if (customers == null || customers.Count == 0)
            {
                return NoContent();
            }

            return Ok(customers);
        }

        [HttpGet, Route("{id:guid}")]
        public async Task<IActionResult> Find([FromRoute] Guid id)
        {
            var customer = await _customersService.FindAsync(id);

            if (customer == null)
            {
                return NoContent();
            }

            return Ok(customer);
        }
    }
}
