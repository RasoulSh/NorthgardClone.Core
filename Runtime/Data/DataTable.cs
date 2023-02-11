using System;
using System.Collections.Generic;
using System.Linq;
using Northgard.Core.EntityBase;
using Zenject;

namespace Northgard.Core.Data
{
    public abstract class DataTable<T> : IDataTable<T> where T : GameEntity
    {
        protected abstract string StorageName { get; } 
        private IDataSaver<T> _saver;
        private List<T> _items;
        public bool IsDirty { get; private set; }


        [Inject]
        private void Init(IDataSaver<T> saver)
        {
            _saver = saver;
            _items = _saver.Load(StorageName) ?? new List<T>();
        }

        public void Create(T newRow)
        {
            _items.Add(newRow);
            MarkAsDirty();
        }

        public void Create(IEnumerable<T> range)
        {
            _items.AddRange(range);
            MarkAsDirty();
        }

        public void CreateOrUpdate(T item)
        {
            if (Exists(item.id))
            {
                Update(item);
            }
            else
            {
                Create(item);
            }
        }

        public T Read(string id)
        {
            return _items.Find(item => item.id == id);
        }

        public IEnumerable<T> Read() => _items;

        public IEnumerable<T> Read(Predicate<T> match)
        {
            return _items.FindAll(match);
        }

        public void Delete(string id)
        {
            var itemToRemove = Read(id);
            _items.Remove(itemToRemove);
            MarkAsDirty();
        }

        public void Delete(Predicate<T> match)
        {
            _items.RemoveAll(match);
            MarkAsDirty();
        }

        public void Update(T row)
        {
            var itemIndexToUpdate = _items.FindIndex(item => item.id == row.id);
            _items[itemIndexToUpdate] = row;
            MarkAsDirty();
        }

        public bool Exists(string id)
        {
            return _items.Any(item => item.id == id);
        }


        public void SaveChanges()
        {
            _saver.Save(_items, StorageName);
            IsDirty = false;
        }

        public void MarkAsDirty()
        {
            IsDirty = true;
        }

        public void RevertChanges()
        {
            _items = _saver.Load(StorageName);
            IsDirty = false;
        }
    }
}