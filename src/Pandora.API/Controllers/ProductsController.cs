using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pandora.API.Resources;
using Pandora.Core.Contracts.Repositories;
using Pandora.Core.Contracts.Services;
using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductResource productResource)
        {
            if (productResource == null)
            {
                productResource = new ProductResource();
            }

            var product = _mapper.Map<ProductResource, Product>(productResource);
            var result = await _productService.AddAsync(product);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut, Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductResource productResource)
        {
            if (productResource == null)
            {
                productResource = new ProductResource();
            }

            var product = _mapper.Map<ProductResource, Product>(productResource);
            var result = await _productService.UpdateAsync(id, product);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete, Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _productService.RemoveAsync(id);

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
            var products = await _productService.FilterAndPaged(skip, take, name);

            if (products == null || products.Count == 0)
            {
                return NoContent();
            }

            return Ok(products);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.ListAsync();

            if (products == null || products.Count == 0)
            {
                return NoContent();
            }

            return Ok(products);
        }

        [HttpGet, Route("{id:guid}")]
        public async Task<IActionResult> Find([FromRoute] Guid id)
        {
            var product = await _productService.FindAsync(id);

            if (product == null)
            {
                return NoContent();
            }

            return Ok(product);
        }
    }
}
