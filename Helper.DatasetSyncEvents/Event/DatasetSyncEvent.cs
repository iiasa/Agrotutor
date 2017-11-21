namespace Helper.DatasetSyncEvents.Event
{
    using Helper.BusinessContract;
    using Prism.Events;

    public class DatasetSyncEvent : PubSubEvent<IDataset>
    {
    }
}