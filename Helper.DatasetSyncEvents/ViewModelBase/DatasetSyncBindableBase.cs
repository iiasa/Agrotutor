namespace Helper.DatasetSyncEvents.ViewModelBase
{
    using Prism.Events;
    using Prism.Mvvm;

    using BusinessContract;
    using Event;

    public abstract class DatasetSyncBindableBase : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        protected DatasetSyncBindableBase(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DatasetAvailableForSyncEvent>().Subscribe(OnDatasetAvailableForSync);
            _eventAggregator.GetEvent<DatasetSyncEvent>().Subscribe(ReadDataset);
        }

        private void OnDatasetAvailableForSync()
        {
            _eventAggregator.GetEvent<DatasetSyncRequestEvent>().Publish();
        }

        protected abstract void ReadDataset(IDataset dataset);

        protected void PublishDataset(IDataset dataset)
        {
            _eventAggregator.GetEvent<DatasetSyncEvent>().Publish(dataset);
        }
    }
}