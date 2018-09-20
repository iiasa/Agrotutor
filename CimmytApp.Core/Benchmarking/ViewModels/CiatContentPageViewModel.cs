namespace CimmytApp.Core.Benchmarking.ViewModels
{
    using System.Collections.Generic;
    using System.Net;
    using CimmytApp.Core.DTO.Benchmarking;
    using CimmytApp.DTO.Parcel;
    using Flurl;
    using Flurl.Http;
    using Helper.Map;
    using Prism.Mvvm;
    using Prism.Navigation;



    public class CiatContentPageViewModel : BindableBase, INavigatedAware
    {
        public static string DEFAULT_NAVIGATION_TITLE = "CiatContentPage";

        public static string PARAMETER_NAME_POSITION = "Position";
        public static string PARAMETER_NAME_CROP = "Crop";
        public static string PARAMETER_NAME_CROP_TYPE = "CropType";
        public static string PARAMETER_NAME_OLD_YIELD = "OldYield";

        public bool IsLoading { get; set; }

        public CiatData Data { get; set; }

        public GeoPosition GeoPosition { get; set; }

        public string Crop { get; set; }

        public CropType CropType { get; set; }

        public string OldYield { get; set; }

        public async void LoadData()
        {
            IsLoading = true;
            var responseData = await "http://104.239.158.49".AppendPathSegment("matrizv2.php")
                .SetQueryParams(new
                {
                    lat = GeoPosition.Latitude,
                    lon = GeoPosition.Longitude,
                    type = "matriz",
                    tkn = "E31C5F8478566357BA6875B32DC59",
                    cultivo = Crop
                }).WithBasicAuth("cimmy2018", "tBTAibgFtHxaNE8ld7hpKKsx3n1ORIO")
                .GetJsonAsync<List<CiatResponseData>>();

            Data = CiatData.FromResponse(responseData);
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

            parameters.TryGetValue(CiatContentPageViewModel.PARAMETER_NAME_OLD_YIELD, out string oldYield);
            this.OldYield = oldYield;

            LoadData();
        }
    }
}
