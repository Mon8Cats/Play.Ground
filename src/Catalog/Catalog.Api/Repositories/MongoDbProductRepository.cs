using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
    public class MongoDbProductRepository : IProductRepository
    {
        private const string databaseName = "catalog";
        private const string collectionName = "products";
        private readonly IMongoCollection<Product> _productCollection;
        private readonly FilterDefinitionBuilder<Product> _filterBuilder = Builders<Product>.Filter;
        public MongoDbProductRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            _productCollection = database.GetCollection<Product>(collectionName);
        }

        public void CreateProduct(Product product)
        {
            _productCollection.InsertOne(product);
        }

        public void DeleteProduct(Guid id)
        {
            var filter = _filterBuilder.Eq(p => p.Id, id);
            _productCollection.DeleteOne(filter);
        }

        public Product GetProduct(Guid id)
        {
            var filter = _filterBuilder.Eq(p => p.Id, id);
            return _productCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productCollection.Find(_filterBuilder.Empty).ToList();
        }

        public void UpdateProduct(Product product)
        {
            var filter = _filterBuilder.Eq(p => p.Id, product.Id);
            _productCollection.ReplaceOne(filter, product);
        }
    }
}