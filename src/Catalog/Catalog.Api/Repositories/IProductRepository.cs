using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
    public interface IProductRepository
    {
        Product GetProduct(Guid id);
        IEnumerable<Product> GetProducts();

        void CreateProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(Guid id);
    }
}