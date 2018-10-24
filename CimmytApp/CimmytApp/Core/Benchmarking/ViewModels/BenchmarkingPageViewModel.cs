namespace CimmytApp.Core.Benchmarking.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CimmytApp.DTO.Benchmarking;
    using CimmytApp.DTO.Parcel;
    using CimmytApp.ViewModels;
    using Helper.GeoWiki.API;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    public class BenchmarkingPageViewModel : ViewModelBase, INavigatedAware
    {
        public BenchmarkingPageViewModel(INavigationService navigationService,
            IStringLocalizer<BenchmarkingPageViewModel> localizer) : base(localizer)
        {
            LoadDataCommand = new DelegateCommand(LoadData);
            this.navigationService = navigationService;
        }

        private readonly INavigationService navigationService;
        private BenchmarkingInformation dataIrrigated;
        private BenchmarkingInformation dataRainfed;
        private bool downloadButtonActive;
        private bool downloading;
        private BenchmarkingInformation benchmarkingInformation;
        private bool noData;
        private Parcel parcel;

        public BenchmarkingInformation DataIrrigated
        {
            get => this.dataIrrigated;
            set => SetProperty(ref this.dataIrrigated, value);
        }

        public BenchmarkingInformation DataRainfed
        {
            get => this.dataRainfed;
            set => SetProperty(ref this.dataRainfed, value);
        }

        public bool DownloadButtonActive
        {
            get => this.downloadButtonActive;
            set => SetProperty(ref this.downloadButtonActive, value);
        }

        public bool Downloading
        {
            get => this.downloading;
            set => SetProperty(ref this.downloading, value);
        }

        public ICommand LoadDataCommand { get; set; }

        public BenchmarkingInformation MyData
        {
            get => this.benchmarkingInformation;
            set
            {
                this.benchmarkingInformation = value;
                UpdateData(value);
            }
        }

        public bool NoData
        {
            get => this.noData;
            set => SetProperty(ref this.noData, value);
        }

        public Parcel Parcel
        {
            get => this.parcel;
            set => SetProperty(ref this.parcel, value);
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
                    this.navigationService.GoBackAsync();
                }
            }
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