using Agrotutor.Modules.GeoWiki;

namespace Agrotutor.Modules.Benchmarking.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    using Core;
    using Core.Entities;
    using Types;

    public class BenchmarkingPageViewModel : ViewModelBase, INavigatedAware
    {
        public BenchmarkingPageViewModel(INavigationService navigationService,
            IStringLocalizer<BenchmarkingPageViewModel> localizer) : base(navigationService, localizer)
        {
            LoadDataCommand = new DelegateCommand(LoadData);
        }

        private readonly INavigationService navigationService;
        private BenchmarkingInformation dataIrrigated;
        private BenchmarkingInformation dataRainfed;
        private bool downloadButtonActive;
        private bool downloading;
        private BenchmarkingInformation benchmarkingInformation;
        private bool noData;
        private Plot plot;

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

        public Plot Plot
        {
            get => this.plot;
            set => SetProperty(ref this.plot, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Plot"))
            {
                parameters.TryGetValue<Plot>("Plot", out var plot);
                if (plot != null)
                {
                    Plot = plot;
                    DownloadButtonActive = true;
                }
                else
                {
                    NavigationService.GoBackAsync();
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
            MyData = await DownloadData((double)Plot.Position.Latitude, (double)Plot.Position.Longitude);
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
