using Sklepix.Data.Entities;

namespace Sklepix.Repositories
{
    public interface IAisleRepository
    {
        List<AisleEntity> GetAisles();
        AisleEntity GetAisleById(int id);
        void InsertAisle(AisleEntity aisleEntity);
        void DeleteAisle(int id);
        void UpdateAisle(AisleEntity aisleEntity);
        void Save();
    }
}
