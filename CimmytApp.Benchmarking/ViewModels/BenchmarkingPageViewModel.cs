using System.Collections.Generic;

namespace CimmytApp.Benchmarking.ViewModels
{
    using DTO.Benchmarking;
    using DTO.Parcel;
    using Helper.BusinessContract;
    using Helper.DatasetSyncEvents.ViewModelBase;
    using Helper.GeoWiki.API;
    using Prism;
    using Prism.Commands;
    using Prism.Events;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// Defines the <see cref="BenchmarkingPageViewModel" />
    /// </summary>
    public class BenchmarkingPageViewModel : DatasetReceiverBindableBase
    {
        /// <summary>
        /// Defines the _parcel
        /// </summary>
        private Parcel _parcel;

        /// <summary>
        /// Defines the _myData
        /// </summary>
        private BenchmarkingInformation _myData;

        private BenchmarkingInformation _dataIrrigated;
        private BenchmarkingInformation _dataRainfed;

        /// <summary>
        /// Gets or sets the Parcel
        /// </summary>
        public Parcel Parcel { get => _parcel; set => SetProperty(ref _parcel, value); }

        private bool _downloadButtonActive;

        public bool DownloadButtonActive
        {
            get => _downloadButtonActive;
            set => SetProperty(ref _downloadButtonActive, value);
        }

        private bool _downloading;

        public bool Downloading
        {
            get => _downloading;
            set => SetProperty(ref _downloading, value);
        }

        private bool _noData;

        public bool NoData
        {
            get => _noData;
            set => SetProperty(ref _noData, value);
        }

        /// <summary>
        /// Gets or sets the LoadDataCommand
        /// </summary>
        public ICommand LoadDataCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkingPageViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The <see cref="IEventAggregator"/></param>
        public BenchmarkingPageViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            LoadDataCommand = new DelegateCommand(LoadData);
        }

        /// <summary>
        /// The LoadData
        /// </summary>
        private async void LoadData()
        {
            DownloadButtonActive = false;
            Downloading = true;
            MyData = await DownloadData(Parcel.Latitude, Parcel.Longitude);
        }

        /// <summary>
        /// The DownloadData
        /// </summary>
        /// <param name="parcelLatitude">The <see cref="double"/></param>
        /// <param name="parcelLongitude">The <see cref="double"/></param>
        /// <returns>The <see cref="Task{BenchmarkingInformation}"/></returns>
        private async Task<BenchmarkingInformation> DownloadData(double parcelLatitude, double parcelLongitude)
        {
            if (parcelLatitude == 0 && parcelLongitude == 0) return null;
            if (parcelLongitude > 0)
            {
                parcelLatitude = 20;
                parcelLongitude = -100;
            }
            try
            {
                var param = new BenchmarkingRequestParams
                {
                    lat = parcelLatitude,
                    lng = parcelLongitude
                };
                var data = await GeoWikiApi.Post<List<BenchmarkingInformation.BenchmarkingDataset>>("Raster", "GetColCimBenchmarkingInformation",
                    param);

                var info = new BenchmarkingInformation { BenchmarkingDatasets = data };
                info.SetYears();
                return info;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the DataIrrigated
        /// </summary>
        public BenchmarkingInformation DataIrrigated
        {
            get => _dataIrrigated;
            set => SetProperty(ref _dataIrrigated, value);
        }

        /// <summary>
        /// Gets or sets the DataRainfed
        /// </summary>
        public BenchmarkingInformation DataRainfed
        {
            get => _dataRainfed;
            set => SetProperty(ref _dataRainfed, value);
        }

        /// <summary>
        /// Gets or sets the MyData
        /// </summary>
        public BenchmarkingInformation MyData
        {
            get => _myData;
            set
            {
                _myData = value;
                UpdateData(value);
            }
        }

        /// <summary>
        /// The UpdateData
        /// </summary>
        /// <param name="value">The <see cref="BenchmarkingInformation"/></param>
        private void UpdateData(BenchmarkingInformation value)
        {
            if (value == null)
            {
                Downloading = false;
                DownloadButtonActive = true;
            }
            if (value.BenchmarkingDatasets.Count == 0)
            {
                NoData = true;
                Downloading = false;
                return;
            }
            var dataIrrigated = new BenchmarkingInformation();
            var dataRainfed = new BenchmarkingInformation();

            dataIrrigated.BenchmarkingDatasets = new List<BenchmarkingInformation.BenchmarkingDataset>();
            dataRainfed.BenchmarkingDatasets = new List<BenchmarkingInformation.BenchmarkingDataset>();

            foreach (var valueBenchmarkingDataset in value.BenchmarkingDatasets)
            {
                if (valueBenchmarkingDataset.filename.Contains("ir"))
                {
                    dataIrrigated.BenchmarkingDatasets.Add(valueBenchmarkingDataset);
                }
                else
                {
                    dataRainfed.BenchmarkingDatasets.Add(valueBenchmarkingDataset);
                }
            }

			dataIrrigated.KeepNewest(5);
			dataRainfed.KeepNewest(5);
            DataIrrigated = dataIrrigated;
            DataRainfed = dataRainfed;
            Downloading = false;
        }

        /// <summary>
        /// The ReadDataset
        /// </summary>
        /// <param name="dataset">The <see cref="IDataset"/></param>
        protected override void ReadDataset(IDataset dataset)
        {
            if (dataset != null)
            {
                Parcel = (Parcel)dataset;
                DownloadButtonActive = true;
            }
        }
    }
}