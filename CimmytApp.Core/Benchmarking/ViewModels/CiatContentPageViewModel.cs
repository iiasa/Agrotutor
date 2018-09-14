using System;
using System.Collections.Generic;
using System.Text;
using Helper.HTTP;

namespace CimmytApp.Core.Benchmarking.ViewModels
{
    using Prism.Mvvm;
    using Prism.Navigation;

    public class CiatContentPageViewModel : BindableBase, INavigatedAware
    {

        public Boolean IsLoading { get; set; }
        public object Data { get; set; }

        public async void LoadData()
        {
            IsLoading = true;
            Data = await RequestJson.Get<object>(
                "http://104.239.158.49/matrizv2.php?lat=20.8724301&lon=-102.0054769&type=matriz&tkn=E31C5F8478566357BA6875B32DC59&cultivo=Maiz");
            IsLoading = false;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

            LoadData();
        }
    }
}
