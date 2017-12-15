namespace Helper.DatasetSyncEvents.ViewModelBase
{
    using Helper.DatasetSyncEvents.Event;
    using Prism.Events;
    using Prism.Mvvm;

    /// <summary>
    ///     Implementation of System.ComponentModel.INotifyPropertyChanged,
    ///     combined with EventAggregator functionality for publishing instances of Helper.IDataset
    /// </summary>
    public abstract class DatasetPublisherBindableBase : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        ///     Constructor for instantiation with reference to the EventAggregator
        /// </summary>
        /// <param name="eventAggregator">Reference to EventAggregator</param>
        protected DatasetPublisherBindableBase(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DatasetSyncRequestEvent>().Subscribe(OnDatasetSyncRequest);
        }

        /// <summary>
        ///     Gets instance of IDataset
        /// </summary>
        /// <returns>Dataset</returns>
        protected abstract object GetDataset();

        /// <summary>
        ///     Publishes an instance of IDataset on the EventAggregator
        /// </summary>
        /// <param name="dataset">Dataset instance to be published</param>
        protected void PublishDataset(object dataset)
        {
            _eventAggregator.GetEvent<DatasetSyncEvent>().Publish(dataset);
        }

        private void OnDatasetSyncRequest()
        {
            PublishDataset(GetDataset());
        }
    }
}