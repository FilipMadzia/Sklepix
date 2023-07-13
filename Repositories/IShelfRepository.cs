using Sklepix.Data.Entities;

namespace Sklepix.Repositories
{
    public interface IShelfRepository
    {
        List<ShelfEntity> GetShelves();
        ShelfEntity GetShelfById(int id);
        void InsertShelf(ShelfEntity shelfEntity);
        void DeleteShelf(int id);
        void UpdateShelf(ShelfEntity shelfEntity);
        void Save();
    }
}
