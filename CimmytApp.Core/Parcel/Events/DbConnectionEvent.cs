namespace CimmytApp.Parcel.Events
{
    using Helper.Realm;
    using Helper.Realm.BusinessContract;
    using Prism.Events;

    public class DbConnectionEvent : PubSubEvent<ICimmytDbOperations>
    {
    }
}