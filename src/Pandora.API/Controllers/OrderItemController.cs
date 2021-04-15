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
    [Route("api/v{version:apiVersion}/order-items")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IMapper _mapper;

        public OrderItemController(IOrderItemService orderItemService, IMapper mapper)
        {
            _orderItemService = orderItemService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderItemResource itemResource)
        {
            if (itemResource == null)
            {
                itemResource = new OrderItemResource();
            }

            var item = _mapper.Map<OrderItemResource, OrderItem>(itemResource);
            await _orderItemService.AddAsync(item);

            return Ok();
        }

        [HttpPut, Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] OrderItemResource itemResource)
        {
            if (itemResource == null)
            {
                itemResource = new OrderItemResource();
            }

            var item = _mapper.Map<OrderItemResource, OrderItem>(itemResource);
            var result = await _orderItemService.UpdateAsync(id, item);

            return Ok(result);
        }

        [HttpDelete, Route("{id:guid}")]
        public async Task<IActionResult> Remove([FromRoute] Guid id)
        {
            await _orderItemService.RemoveAsync(id);
            return Ok();
        }
    }
}
