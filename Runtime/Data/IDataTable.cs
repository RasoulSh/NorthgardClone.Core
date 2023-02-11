using Northgard.Core.EntityBase;

namespace Northgard.Core.Data
{
    public interface IDataTable<T> : IPersistentStorage<T> where T : GameEntity
    {
        bool IsDirty { get; }
        void SaveChanges();
        void MarkAsDirty();
        void RevertChanges();
    }
}