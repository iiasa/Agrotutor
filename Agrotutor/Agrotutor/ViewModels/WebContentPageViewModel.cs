using Agrotutor.Core;
using Agrotutor.Core.DependencyServices;
using Microsoft.Extensions.Localization;
using Prism.Navigation;
using Xamarin.Forms;

namespace Agrotutor.ViewModels
{
    public class WebContentPageViewModel : ViewModelBase
    {
        public WebContentPageViewModel(INavigationService navigationService, IStringLocalizer<WebContentPageViewModel> stringLocalizer) 
            : base(navigationService, stringLocalizer)
        {
            var rootUrl = DependencyService.Get<IHtmlBaseUrl>().Get();
            Url = $"{rootUrl}about/page.html";
        }

        private string _url;

        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }
    }
}
