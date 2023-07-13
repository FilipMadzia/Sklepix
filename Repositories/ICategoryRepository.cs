using Sklepix.Data.Entities;

namespace Sklepix.Repositories
{
    public interface ICategoryRepository
    {
        List<CategoryEntity> GetCategories();
        CategoryEntity GetCategoryById(int id);
        void InsertCategory(CategoryEntity categoryEntity);
        void DeleteCategory(int id);
        void UpdateCategory(CategoryEntity categoryEntity);
        void Save();
    }
}
