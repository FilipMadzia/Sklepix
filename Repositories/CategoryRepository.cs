using Microsoft.EntityFrameworkCore;
using Sklepix.Data.Entities;
using Sklepix.Data;

namespace Sklepix.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly SklepixContext _context;

		public CategoryRepository(SklepixContext context)
		{
			_context = context;
		}

		public List<CategoryEntity> GetCategories()
		{
			return _context.CategoryEntity
				.ToList();
		}

		public CategoryEntity GetCategoryById(int id)
		{
			return _context.CategoryEntity
				.First(x => x.Id == id);
		}

		public void InsertCategory(CategoryEntity categoryEntity)
		{
			_context.Add(categoryEntity);
		}

		public void DeleteCategory(int id)
		{
			_context.Remove(_context.CategoryEntity.Find(id));
		}

		public void UpdateCategory(CategoryEntity categoryfEntity)
		{
			_context.Entry(categoryfEntity).State = EntityState.Modified;
		}

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
