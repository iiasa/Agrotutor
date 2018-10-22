namespace CimmytApp.Benchmarking.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CimmytApp.DTO.Benchmarking;
    using CimmytApp.DTO.Parcel;
    using Helper.GeoWiki.API;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class BenchmarkingPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private BenchmarkingInformation _dataIrrigated;
        private BenchmarkingInformation _dataRainfed;
        private bool _downloadButtonActive;
        private bool _downloading;
        private BenchmarkingInformation _myData;
        private bool _noData;
        private Parcel _parcel;

        public BenchmarkingPageViewModel(INavigationService navigationService)
        {
            LoadDataCommand = new DelegateCommand(LoadData);
            _navigationService = navigationService;
        }

        public BenchmarkingInformation DataIrrigated
        {
            get => _dataIrrigated;
            set => SetProperty(ref _dataIrrigated, value);
        }

        public BenchmarkingInformation DataRainfed
        {
            get => _dataRainfed;
            set => SetProperty(ref _dataRainfed, value);
        }

        public bool DownloadButtonActive
        {
            get => _downloadButtonActive;
            set => SetProperty(ref _downloadButtonActive, value);
        }

        public bool Downloading
        {
            get => _downloading;
            set => SetProperty(ref _downloading, value);
        }

        public ICommand LoadDataCommand { get; set; }

        public BenchmarkingInformation MyData
        {
            get => _myData;
            set
            {
                _myData = value;
                UpdateData(value);
            }
        }

        public bool NoData
        {
            get => _noData;
            set => SetProperty(ref _noData, value);
        }

        public Parcel Parcel
        {
            get => _parcel;
            set => SetProperty(ref _parcel, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue<Parcel>("Parcel", out var parcel);
                if (parcel != null)
                {
                    Parcel = parcel;
                    DownloadButtonActive = true;
                }
                else
                {
                    _navigationService.GoBackAsync();
                }
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

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
                var data = await GeoWikiApi.Post<List<BenchmarkingInformation.BenchmarkingDataset>>("Raster",
                    "GetColCimBenchmarkingInformation", param);

                var info = new BenchmarkingInformation
                {
                    BenchmarkingDatasets = data
                };
                info.SetYears();
                return info;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async void LoadData()
        {
            DownloadButtonActive = false;
            Downloading = true;
            MyData = await DownloadData((double)Parcel.Position.Latitude, (double)Parcel.Position.Longitude);
        }

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
    }
}