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
    [Route("api/v{version:apiVersion}/payment-methods")]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IPaymentMethodsService _paymentMethodsService;
        private readonly IMapper _mapper;

        public PaymentMethodsController(IPaymentMethodsService paymentMethodsService, IMapper mapper)
        {
            _paymentMethodsService = paymentMethodsService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentMethodResource paymentMethodResource)
        {
            var paymentMethod = _mapper.Map<PaymentMethodResource, PaymentMethod>(paymentMethodResource);
            var result = await _paymentMethodsService.AddAsync(paymentMethod);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut, Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] PaymentMethodResource paymentMethodResource)
        {
            var paymentMethod = _mapper.Map<PaymentMethodResource, PaymentMethod>(paymentMethodResource);
            var result = await _paymentMethodsService.UpdateAsync(id, paymentMethod);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete, Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _paymentMethodsService.RemoveAsync(id);

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
            var paymentMethods = await _paymentMethodsService.FilterAndPaged(skip, take, name);

            if (paymentMethods == null || paymentMethods.Count == 0)
            {
                return NoContent();
            }

            return Ok(paymentMethods);
        }

        [HttpGet, Route("{id:guid}")]
        public async Task<IActionResult> Find([FromRoute] Guid id)
        {
            var paymentMethod = await _paymentMethodsService.FindAsync(id);

            if (paymentMethod == null)
            {
                return NoContent();
            }

            return Ok(paymentMethod);
        }
    }
}
