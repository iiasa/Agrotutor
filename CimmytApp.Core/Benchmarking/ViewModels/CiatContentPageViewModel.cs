namespace CimmytApp.Core.Benchmarking.ViewModels
{
    using System;
    using System.Collections.Generic;
    using CimmytApp.Core.DTO.Benchmarking;
    using CimmytApp.DTO.Parcel;
    using Flurl;
    using Flurl.Http;
    using Helper.Map;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class CiatContentPageViewModel : BindableBase, INavigatedAware
    {
        public CiatContentPageViewModel()
        {
            IrrigatedClickedCommand = new DelegateCommand(IrrigatedClicked);
            NonIrrigatedClickedCommand = new DelegateCommand(NonIrrigatedClicked);
            NoData = false;
        }

        public static string DEFAULT_NAVIGATION_TITLE = "CiatContentPage";

        public static string PARAMETER_NAME_POSITION = "Position";
        public static string PARAMETER_NAME_CROP = "Crop";
        public static string PARAMETER_NAME_CROP_TYPE = "CropType";
        public static string PARAMETER_NAME_OLD_YIELD = "OldYield";

        private CiatData.CiatDataDetail currentData;
        private bool isLoading;
        private bool noData;
        private CiatData data;

        public DelegateCommand IrrigatedClickedCommand { get; set; }
        public DelegateCommand NonIrrigatedClickedCommand { get; set; }

        public bool IsLoading
        {
            get => this.isLoading;
            set => SetProperty(ref this.isLoading, value);
        }

        public bool NoData
        {
            get => this.noData;
            set => SetProperty(ref this.noData, value);
        }

        public CiatData Data
        {
            get => this.data;
            set => SetProperty(ref this.data, value);
        }

        public CiatData.CiatDataDetail CurrentData
        {
            get => this.currentData;
            set => SetProperty(ref this.currentData, value);
        }

        public GeoPosition GeoPosition { get; set; }

        public string Crop { get; set; }

        public CropType CropType { get; set; }

        public double OldYield { get; set; }

        public async void LoadData()
        {
            IsLoading = true;
            var request = "http://104.239.158.49".AppendPathSegment("matrizv2.php")
                .SetQueryParams(new
                {
                    lat = GeoPosition.Latitude,
                    lon = GeoPosition.Longitude,
                    type = "matriz",
                    tkn = "E31C5F8478566357BA6875B32DC59",
                    cultivo = Crop
                }).WithBasicAuth("cimmy2018", "tBTAibgFtHxaNE8ld7hpKKsx3n1ORIO");

            try
            {
                List<CiatResponseData> responseData = await request.GetJsonAsync<List<CiatResponseData>>();
                Data = CiatData.FromResponse(responseData, request.Url);
                IrrigatedClickedCommand.Execute();
            }
            catch
            {
                Data = null;
                NoData = true;
            }

            IsLoading = false;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            parameters.TryGetValue(CiatContentPageViewModel.PARAMETER_NAME_POSITION, out GeoPosition position);
            this.GeoPosition = position;

            parameters.TryGetValue(CiatContentPageViewModel.PARAMETER_NAME_CROP, out string crop);
            this.Crop = crop;

            parameters.TryGetValue(CiatContentPageViewModel.PARAMETER_NAME_CROP_TYPE, out CropType cropType);
            this.CropType = cropType;

            parameters.TryGetValue(CiatContentPageViewModel.PARAMETER_NAME_OLD_YIELD, out double oldYield);
            this.OldYield = oldYield;

            LoadData();
        }

        private void IrrigatedClicked()
        {
            CurrentData = Data?.CiatDataIrrigated;
        }

        private void NonIrrigatedClicked()
        {
            CurrentData = Data?.CiatDataNonIrrigated;
        }
    }
}
