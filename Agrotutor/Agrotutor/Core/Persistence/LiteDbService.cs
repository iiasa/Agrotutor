﻿using System.Collections.Generic;
using System.IO;
using LiteDB;
using Xamarin.Essentials;

namespace Agrotutor.Core.Persistence
{
    public class LiteDbService<T> : IDbService<T>
    {
        protected LiteCollection<T> Collection;

        public LiteDbService()
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, Constants.OFFLINE_DATABASE_NAME);

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            var db = new LiteDatabase(path);
            Collection = db.GetCollection<T>();
        }

        public virtual T CreateItem(T item)
        {
            var val = Collection.Insert(item);
            return item;
        }
        public virtual T UpdateItem(T item)
        {
            Collection.Update(item);
            return item;
        }

        public bool DeleteItem(BsonValue item)
        {
            return Collection.Delete(item);
        }

        public virtual IEnumerable<T> ReadAllItems()
        {
            var all = Collection.FindAll();
            return new List<T>(all);
        }

        public T DeleteItem(T item)
        {
            var c = Collection.Delete(i => i.Equals(item));
            return item;
        }
        public bool DeleteItem(int id)
        {
            return Collection.Delete(id);
        }
    }
}