namespace Helper.DatasetSyncEvents.ViewModelBase
{
    using Prism.Events;
    using Prism.Mvvm;

    using BusinessContract;
    using Event;

    /// <summary>
    /// Implementation of System.ComponentModel.INotifyPropertyChanged,
    /// combined with EventAggregator functionality for publishing instances of Helper.IDataset
    /// </summary>
    public abstract class DatasetPublisherBindableBase : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Constructor for instantiation with reference to the EventAggregator
        /// </summary>
        /// <param name="eventAggregator">Reference to EventAggregator</param>
        protected DatasetPublisherBindableBase(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DatasetSyncRequestEvent>().Subscribe(OnDatasetSyncRequest);
        }

        private void OnDatasetSyncRequest()
        {
            PublishDataset(GetDataset());
        }

        /// <summary>
        /// Gets instance of IDataset
        /// </summary>
        /// <returns>Dataset</returns>
        protected abstract IDataset GetDataset();

        /// <summary>
        /// Publishes an instance of IDataset on the EventAggregator
        /// </summary>
        /// <param name="dataset">Dataset instance to be published</param>
        protected void PublishDataset(IDataset dataset)
        {
            _eventAggregator.GetEvent<DatasetSyncEvent>().Publish(dataset);
        }
    }
}