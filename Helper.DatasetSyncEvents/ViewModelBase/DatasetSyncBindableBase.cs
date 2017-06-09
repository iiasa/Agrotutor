﻿namespace Helper.DatasetSyncEvents.ViewModelBase
{
    using Prism.Events;
    using Prism.Mvvm;

    using BusinessContract;
    using Event;
    using System;

    /// <summary>
    /// Implementation of System.ComponentModel.INotifyPropertyChanged,
    /// combined with EventAggregator functionality for publishing and receiving instances of Helper.IDataset
    /// </summary>
    public abstract class DatasetSyncBindableBase : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private int _lastDatasetReceivedHash;

        /// <summary>
        /// Constructor for instantiation with reference to the EventAggregator
        /// </summary>
        /// <param name="eventAggregator">Reference to EventAggregator</param>
        protected DatasetSyncBindableBase(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DatasetAvailableForSyncEvent>().Subscribe(OnDatasetAvailableForSync);
            _eventAggregator.GetEvent<DatasetSyncEvent>().Subscribe(OnDatasetReceived);
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

        private void OnDatasetReceived(IDataset dataset)
        {
            _lastDatasetReceivedHash = dataset.GetHashCode();
            ReadDataset(dataset);
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

        /// <summary>
        /// Publishes an instance of IDataset on the EventAggregator
        /// </summary>
        /// <param name="dataset">Dataset instance to be published</param>
        protected void PublishDataset(IDataset dataset)
        {
            if (dataset.GetHashCode() == _lastDatasetReceivedHash) return; //Prevent from sending datasets which were just received
            _eventAggregator.GetEvent<DatasetSyncEvent>().Publish(dataset);
        }
    }
}