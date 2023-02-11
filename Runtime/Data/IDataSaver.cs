using System.Collections.Generic;
using Northgard.Core.EntityBase;

namespace Northgard.Core.Data
{
    public interface IDataSaver<T> where T : GameEntity
    {
        void Save(List<T> data, string storageName);
        List<T> Load(string storageName);
        void Delete(string storageName);
    }
}