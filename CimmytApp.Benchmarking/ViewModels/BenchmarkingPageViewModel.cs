namespace CimmytApp.Benchmarking.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Navigation;

    using DTO.Benchmarking;
    using DTO.Parcel;
    using Helper.GeoWiki.API;

    /// <summary>
    /// Defines the <see cref="BenchmarkingPageViewModel" />
    /// </summary>
    public class BenchmarkingPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// Defines the _parcel
        /// </summary>
        private Parcel _parcel;

        /// <summary>
        /// Defines the _myData
        /// </summary>
        private BenchmarkingInformation _myData;

        /// <summary>
        /// Defines the _dataIrrigated
        /// </summary>
        private BenchmarkingInformation _dataIrrigated;

        /// <summary>
        /// Defines the _dataRainfed
        /// </summary>
        private BenchmarkingInformation _dataRainfed;

        /// <summary>
        /// Gets or sets the Parcel
        /// </summary>
        public Parcel Parcel { get => _parcel; set => SetProperty(ref _parcel, value); }

        /// <summary>
        /// Defines the _downloadButtonActive
        /// </summary>
        private bool _downloadButtonActive;

        /// <summary>
        /// Gets or sets a value indicating whether DownloadButtonActive
        /// </summary>
        public bool DownloadButtonActive { get => _downloadButtonActive; set => SetProperty(ref _downloadButtonActive, value); }

        /// <summary>
        /// Defines the _downloading
        /// </summary>
        private bool _downloading;

        /// <summary>
        /// Gets or sets a value indicating whether Downloading
        /// </summary>
        public bool Downloading { get => _downloading; set => SetProperty(ref _downloading, value); }

        /// <summary>
        /// Defines the _noData
        /// </summary>
        private bool _noData;

        private INavigationService _navigationService;

        /// <summary>
        /// Gets or sets a value indicating whether NoData
        /// </summary>
        public bool NoData { get => _noData; set => SetProperty(ref _noData, value); }

        /// <summary>
        /// Gets or sets the LoadDataCommand
        /// </summary>
        public ICommand LoadDataCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkingPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService"></param>
        public BenchmarkingPageViewModel(INavigationService navigationService)
        {
            LoadDataCommand = new DelegateCommand(LoadData);
            _navigationService = navigationService;
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
            if (parcelLongitude >= 0)
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
        public BenchmarkingInformation DataIrrigated { get => _dataIrrigated; set => SetProperty(ref _dataIrrigated, value); }

        /// <summary>
        /// Gets or sets the DataRainfed
        /// </summary>
        public BenchmarkingInformation DataRainfed { get => _dataRainfed; set => SetProperty(ref _dataRainfed, value); }

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

        /// <inheritdoc />
        /// <summary>
        /// The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="T:Prism.Navigation.NavigationParameters" /></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="T:Prism.Navigation.NavigationParameters" /></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue("Parcel", out var parcel);
                if (parcel != null)
                {
                    Parcel = (Parcel)parcel;
                    DownloadButtonActive = true;
                }
                else
                {
                    _navigationService.GoBackAsync();
                }
            }
        }
    }
}