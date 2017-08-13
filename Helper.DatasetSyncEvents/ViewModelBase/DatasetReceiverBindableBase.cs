﻿namespace Helper.DatasetSyncEvents.ViewModelBase
{
    using Prism.Events;
    using Prism.Mvvm;

    using BusinessContract;
    using Event;

    /// <summary>
    /// Implementation of System.ComponentModel.INotifyPropertyChanged,
    /// combined with EventAggregator functionality for receiving instances of Helper.IDataset
    /// </summary>
    public abstract class DatasetReceiverBindableBase : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Constructor for instantiation with reference to the EventAggregator
        /// </summary>
        /// <param name="eventAggregator">Reference to EventAggregator</param>
        protected DatasetReceiverBindableBase(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DatasetAvailableForSyncEvent>().Subscribe(OnDatasetAvailableForSync);
            _eventAggregator.GetEvent<DatasetSyncEvent>().Subscribe(ReadDataset);
            //_eventAggregator.GetEvent<DatasetSyncRequestEvent>().Publish();
        }

        /// <summary>
        /// Requests updated dataset from publisher
        /// </summary>
        private void OnDatasetAvailableForSync()
        {
            _eventAggregator.GetEvent<DatasetSyncRequestEvent>().Publish();
        }

        /// <summary>
        /// Method called on receive dataset.
        /// </summary>
        /// <param name="dataset">Dataset received</param>
        protected abstract void ReadDataset(IDataset dataset);
    }
}