using System;
using System.Collections.Generic;
using System.Text;
using Helper.HTTP;

namespace CimmytApp.Core.Benchmarking.ViewModels
{
    using System.Net;
    using CimmytApp.Core.DTO.Benchmarking;
    using CimmytApp.DTO.Parcel;
    using Helper.Datatypes;
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

        public Boolean IsLoading { get; set; }

        public object Data { get; set; }

        public GeoPosition GeoPosition { get; set; }

        public string Crop { get; set; }

        public CropType CropType { get; set; }

        public String OldYield { get; set; }

        public async void LoadData()
        {
            IsLoading = true;
            String url = $"http://104.239.158.49/matrizv2.php?lat={GeoPosition.Latitude}&lon={GeoPosition.Longitude}&type=matriz&tkn=E31C5F8478566357BA6875B32DC59&cultivo={Crop}";
            Data = await RequestJson.Get<List<CiatData>>(url, new NetworkCredential("cimmy2018", "tBTAibgFtHxaNE8ld7hpKKsx3n1ORIO"));
            IsLoading = false;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            GeoPosition position;
            parameters.TryGetValue(CiatContentPageViewModel.PARAMETER_NAME_POSITION, out position);
            this.GeoPosition = position;

            string crop;
            parameters.TryGetValue(CiatContentPageViewModel.PARAMETER_NAME_CROP, out crop);
            this.Crop = crop;

            CropType cropType;
            parameters.TryGetValue(CiatContentPageViewModel.PARAMETER_NAME_CROP_TYPE, out cropType);
            this.CropType = cropType;

            String oldYield;
            parameters.TryGetValue(CiatContentPageViewModel.PARAMETER_NAME_OLD_YIELD, out oldYield);
            this.OldYield = oldYield;

            LoadData();
        }
    }
}
