using System;
using System.Collections.Generic;
using Northgard.Core.EntityBase;

namespace Northgard.Core.Data
{
    public interface IPersistentStorage<T> where T : GameEntity
    {
        void Create(T newItem);
        void Create(IEnumerable<T> range);
        void CreateOrUpdate(T item);
        T Read(string id);
        IEnumerable<T> Read();
        IEnumerable<T> Read(Predicate<T> match);
        void Delete(string id);
        void Delete(Predicate<T> match);
        void Update(T item);
        bool Exists(string id);
    }
}