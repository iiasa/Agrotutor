namespace Helper.Realm
{
    using System.Collections.Generic;

    public interface IPersistedObject
    {
        void Persist();
        IPersistedObject GetById(int id);
        List<IPersistedObject> GetAll();
        void Delete(int id);
    }
}
