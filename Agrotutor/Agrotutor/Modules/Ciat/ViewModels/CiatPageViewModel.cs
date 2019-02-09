namespace Agrotutor.Modules.Ciat.ViewModels
{
    using System.Collections.Generic;
    using Flurl;
    using Flurl.Http;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    using Core;
    using Core.Entities;
    using Types;

    public class CiatPageViewModel : ViewModelBase, INavigatedAware
    {
        public static string DEFAULT_NAVIGATION_TITLE = "CiatContentPage";

        public static string PARAMETER_NAME_CROP = "Crop";

        public static string PARAMETER_NAME_CROP_TYPE = "CropType";

        public static string PARAMETER_NAME_OLD_YIELD = "OldYield";

        public static string PARAMETER_NAME_POSITION = "Position";

        private CiatData.CiatDataDetail currentData;

        private CiatData data;

        private bool isLoading;

        private bool noData;

        public CiatPageViewModel(INavigationService navigationService, IStringLocalizer<CiatPageViewModel> localizer)
            : base(navigationService, localizer)
        {
            IrrigatedClickedCommand = new DelegateCommand(IrrigatedClicked);
            NonIrrigatedClickedCommand = new DelegateCommand(NonIrrigatedClicked);
            NoData = false;
        }

        public string Crop { get; set; }

        public CropType CropType { get; set; }

        public CiatData.CiatDataDetail CurrentData
        {
            get => this.currentData;
            set => SetProperty(ref this.currentData, value);
        }

        public CiatData Data
        {
            get => this.data;
            set => SetProperty(ref this.data, value);
        }

        public Position GeoPosition { get; set; }

        public DelegateCommand IrrigatedClickedCommand { get; set; }

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

        public DelegateCommand NonIrrigatedClickedCommand { get; set; }

        public double OldYield { get; set; }

        public async void LoadData()
        {
            IsLoading = true;
            IFlurlRequest request = "http://104.239.158.49".AppendPathSegment("matrizv2.php")
                .SetQueryParams(
                    new
                    {
                        lat = GeoPosition.Latitude,
                        lon = GeoPosition.Longitude,
                        type = "matriz",
                        tkn = "E31C5F8478566357BA6875B32DC59",
                        cultivo = Crop
                    })
                .WithBasicAuth("cimmy2018", "tBTAibgFtHxaNE8ld7hpKKsx3n1ORIO");

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

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            parameters.TryGetValue(PARAMETER_NAME_POSITION, out Position position);
            GeoPosition = position;

            parameters.TryGetValue(PARAMETER_NAME_CROP, out string crop);
            Crop = crop;

            parameters.TryGetValue(PARAMETER_NAME_CROP_TYPE, out CropType cropType);
            CropType = cropType;

            parameters.TryGetValue(PARAMETER_NAME_OLD_YIELD, out double oldYield);
            OldYield = oldYield;

            LoadData();

            base.OnNavigatedTo(parameters);
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
