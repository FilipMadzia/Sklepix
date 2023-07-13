using Sklepix.Data.Entities;

namespace Sklepix.Repositories
{
    public interface IProductRepository
    {
        List<ProductEntity> GetProducts();
        ProductEntity GetProductById(int id);
        void InsertProduct(ProductEntity productEntity);
        void DeleteProduct(int id);
        void UpdateProduct(ProductEntity productEntity);
        void Save();
    }
}
