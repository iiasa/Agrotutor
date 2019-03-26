using System.Collections.Generic;
using LiteDB;

namespace Agrotutor.Core.Persistence
{
    public interface IDbService<T>
    {
        T CreateItem(T item);

        T UpdateItem(T item);

        T DeleteItem(T item);
        bool DeleteItem(BsonValue item);

        IEnumerable<T> ReadAllItems();
    }
}