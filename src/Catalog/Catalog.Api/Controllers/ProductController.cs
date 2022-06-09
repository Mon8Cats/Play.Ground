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
        public IEnumerable<ProductDto> GetProducts()
        {
            var products = _repository.GetProducts().Select(p => p.AsDto());

            return products;
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProduct(Guid id)
        {
            var product = _repository.GetProduct(id);

            if(product == null)
            {
                return NotFound();
            }
            return Ok(product.AsDto());
        }

        [HttpPost]
        public ActionResult<ProductDto> CreateProduct(CreateProductDto productDto)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Price = productDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            _repository.CreateProduct(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product.AsDto());
        }


        [HttpPut("{id}")]
        public ActionResult UpdateProduct(Guid id, UpdateProductDto productDto)
        {
            var existingProduct = _repository.GetProduct(id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            Product updatedProduct = existingProduct with {
                Name = productDto.Name,
                Price = productDto.Price
            };


            _repository.UpdateProduct(updatedProduct);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(Guid id)
        {
            var existingProduct = _repository.GetProduct(id);

            if (existingProduct is null)
            {
                return NotFound();
            }

            _repository.DeleteProduct(id);

            return NoContent();
        }

    }
}