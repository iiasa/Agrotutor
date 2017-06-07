namespace Helper.DatasetSyncEvents.ViewModelBase
{
    using Prism.Mvvm;
    using Prism.Events;

    using BusinessContract;
    using Event;

    public abstract class DatasetPublisherBindableBase : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        protected DatasetPublisherBindableBase(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        protected void PublishDataset(IDataset dataset)
        {
            _eventAggregator.GetEvent<DatasetSyncEvent>().Publish(dataset);
        }
    }
}