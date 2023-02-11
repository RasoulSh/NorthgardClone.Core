using Northgard.Core.EntityBase;

namespace Northgard.Core.Data
{
    public interface IRepository<T> : IPersistentStorage<T> where T : GameEntity
    {
        void SaveChanges();
    }
}