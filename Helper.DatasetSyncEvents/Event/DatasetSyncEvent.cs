namespace Helper.DatasetSyncEvents.Event
{
    using Prism.Events;

    using BusinessContract;

    public class DatasetSyncEvent : PubSubEvent<IDataset>
    {
    }
}