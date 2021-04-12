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
    [Route("api/v{version:apiVersion}/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderResource orderResource)
        {
            if (orderResource == null)
            {
                orderResource = new OrderResource();
            }

            var order = _mapper.Map<OrderResource, Order>(orderResource);
            var result = await _orderService.AddAsync(order);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut, Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] OrderResource orderResource)
        {
            if (orderResource == null)
            {
                orderResource = new OrderResource();
            }

            var order = _mapper.Map<OrderResource, Order>(orderResource);
            var result = await _orderService.UpdateAsync(id, order);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut, Route("{id:guid}/finish")]
        public async Task<IActionResult> Finish([FromRoute] Guid id)
        {
            var result = await _orderService.FinishAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut, Route("{id:guid}/deliver")]
        public async Task<IActionResult> Deliver([FromRoute] Guid id)
        {
            var result = await _orderService.DeliverAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut, Route("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel([FromRoute] Guid id)
        {
            var result = await _orderService.CancelAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> FilterAndPage(
            [FromQuery] string search,
            [FromQuery] int skip = 0,
            [FromQuery] int take = 10)
        {
            var orders = await _orderService.FilterAsync(search, skip, take);

            if (orders == null || orders.Count == 0)
            {
                return NoContent();
            }

            return Ok(orders);
        }

        [HttpGet, Route("{id:guid}")]
        public async Task<IActionResult> Find([FromRoute] Guid id)
        {
            var order = await _orderService.FindAsync(id);

            if (order == null)
            {
                return NoContent();
            }

            return Ok(order);
        }
    }
}
