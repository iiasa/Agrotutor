namespace CimmytApp.Parcel.Events
{
    using CimmytApp.BusinessContract;
    using Prism.Events;

    public class DbConnectionEvent : PubSubEvent<ICimmytDbOperations>
    {
    }
}