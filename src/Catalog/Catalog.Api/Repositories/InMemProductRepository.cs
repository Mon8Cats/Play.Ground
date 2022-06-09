using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{

    public class InMemProductRepository : IProductRepository
    {
        private readonly List<Product> products = new()
        {
            new Product { Id = Guid.NewGuid(), Name = "Potion", Price= 9, CreatedDate = DateTimeOffset.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Iron Sword", Price= 20, CreatedDate = DateTimeOffset.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Bronze Shield ", Price= 18, CreatedDate = DateTimeOffset.UtcNow },
        };

        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        public Product GetProduct(Guid id)
        {
            Console.WriteLine(id);
            return products.Where(p => p.Id == id).SingleOrDefault();
        }

        public void CreateProduct(Product product)
        {
            products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            var index = products.FindIndex(p => p.Id == product.Id);

            products[index] = product;

        }

        public void DeleteProduct(Guid id)
        {
            var index = products.FindIndex(p => p.Id == id);

            products.RemoveAt(index);
        }
    }
}