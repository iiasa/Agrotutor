namespace Helper.PublishSubscriberEvents
{
    using Prism.Events;
    using CimmytApp.BusinessContract;

    public class ParcelInSyncEvent : PubSubEvent<IDataset>
    {
    }
}