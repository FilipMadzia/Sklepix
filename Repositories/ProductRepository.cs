using Microsoft.EntityFrameworkCore;
using Sklepix.Data;
using Sklepix.Data.Entities;

namespace Sklepix.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SklepixContext _context;

        public ProductRepository(SklepixContext context)
        {
            _context = context;
        }

        public List<ProductEntity> GetProducts()
        {
            return _context.ProductEntity
                .Include(m => m.Category)
                .Include(m => m.Shelf)
                .Include(m => m.Shelf.Aisle)
                .ToList();
        }

        public ProductEntity GetProductById(int id)
        {
            return _context.ProductEntity
                .Include(m => m.Category)
                .Include(m => m.Shelf)
                .Include(m => m.Shelf.Aisle)
                .First(x => x.Id == id);
        }

        public void InsertProduct(ProductEntity productEntity)
        {
            _context.Add(productEntity);
        }

        public void DeleteProduct(int id)
        {
            _context.Remove(_context.ProductEntity.Find(id));
        }

        public void UpdateProduct(ProductEntity productEntity)
        {
            _context.Entry(productEntity).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
