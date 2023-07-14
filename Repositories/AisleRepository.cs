using Microsoft.EntityFrameworkCore;
using Sklepix.Data.Entities;
using Sklepix.Data;

namespace Sklepix.Repositories
{
	public class AisleRepository : IAisleRepository
	{
		private readonly SklepixContext _context;

		public AisleRepository(SklepixContext context)
		{
			_context = context;
		}

		public List<AisleEntity> GetAisles()
		{
			return _context.AisleEntity
				.ToList();
		}

		public AisleEntity GetAisleById(int id)
		{
			return _context.AisleEntity
				.First(x => x.Id == id);
		}

		public void InsertAisle(AisleEntity aisleEntity)
		{
			_context.Add(aisleEntity);
		}

		public void DeleteAisle(int id)
		{
			_context.Remove(_context.AisleEntity.Find(id));
		}

		public void UpdateAisle(AisleEntity aisleEntity)
		{
			_context.Entry(aisleEntity).State = EntityState.Modified;
		}

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
