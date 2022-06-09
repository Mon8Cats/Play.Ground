using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Api.Dtos;
using Catalog.Api.Entities;
using Catalog.Api.Helpers;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            //repository = new InMemProductRepository();
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = (await _repository.GetProductsAsync())
                            .Select(p => p.AsDto());

            return products;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductAsync(Guid id)
        {
            var product = await _repository.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProductAsync(CreateProductDto productDto)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Price = productDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _repository.CreateProductAsync(product);

            return CreatedAtAction(nameof(GetProductAsync), new { id = product.Id }, product.AsDto());
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProductAsync(Guid id, UpdateProductDto productDto)
        {
            var existingProduct = await _repository.GetProductAsync(id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            Product updatedProduct = existingProduct with
            {
                Name = productDto.Name,
                Price = productDto.Price
            };


            await _repository.UpdateProductAsync(updatedProduct);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductAsync(Guid id)
        {
            var existingProduct = await _repository.GetProductAsync(id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            await _repository.DeleteProductAsync(id);

            return NoContent();
        }

    }
}