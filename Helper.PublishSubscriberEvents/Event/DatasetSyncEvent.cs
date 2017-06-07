namespace Helper.PublishSubscriberEvents
{
    using Prism.Events;
    using CimmytApp.BusinessContract;

    public class DatasetSyncEvent : PubSubEvent<IDataset>
    {
    }
}