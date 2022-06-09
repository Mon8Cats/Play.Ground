using Catalog.Api.Entities;
//using System.Threading.Tasks;

namespace Catalog.Api.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(Guid id);
        Task<IEnumerable<Product>> GetProductsAsync();

        Task CreateProductAsync(Product product);

        Task UpdateProductAsync(Product product);

        Task DeleteProductAsync(Guid id);
    }
}