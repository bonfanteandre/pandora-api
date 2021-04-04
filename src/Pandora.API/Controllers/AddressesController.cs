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
    public class AddressesController : ControllerBase
    {
        private readonly IAddressesService _addressesService;
        private readonly IMapper _mapper;

        public AddressesController(IAddressesService addressesService, IMapper mapper)
        {
            _addressesService = addressesService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddressResource addressResource)
        {
            if (addressResource == null)
            {
                addressResource = new AddressResource();
            }

            var address = _mapper.Map<AddressResource, Address>(addressResource);
            var result = await _addressesService.AddAsync(address);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut, Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AddressResource addressResource)
        {
            if (addressResource == null)
            {
                addressResource = new AddressResource();
            }

            var address = _mapper.Map<AddressResource, Address>(addressResource);
            var result = await _addressesService.UpdateAsync(id, address);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete, Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _addressesService.RemoveAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
