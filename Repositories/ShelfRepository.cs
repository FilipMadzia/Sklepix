using Microsoft.EntityFrameworkCore;
using Sklepix.Data;
using Sklepix.Data.Entities;

namespace Sklepix.Repositories
{
	public class ShelfRepository : IShelfRepository
	{
		private readonly SklepixContext _context;

		public ShelfRepository(SklepixContext context)
		{
			_context = context;
		}

		public List<ShelfEntity> GetShelves()
		{
			return _context.ShelfEntity
				.OrderBy(e => e.Aisle.Id)
				.Include(m => m.Aisle)
				.ToList();
		}

		public ShelfEntity GetShelfById(int id)
		{
			return _context.ShelfEntity
				.Include(m => m.Aisle)
				.First(x => x.Id == id);
		}

		public void InsertShelf(ShelfEntity shelfEntity)
		{
			_context.Add(shelfEntity);
		}

		public void DeleteShelf(int id)
		{
			_context.Remove(_context.ShelfEntity.Find(id));
		}

		public void UpdateShelf(ShelfEntity shelfEntity)
		{
			_context.Entry(shelfEntity).State = EntityState.Modified;
		}

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
