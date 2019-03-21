using Microsoft.AppCenter.Analytics;
using Prism.Common;
using Xamarin.Forms;

namespace Agrotutor.Core
{
    using Prism.Mvvm;
    using Prism.Navigation;
    using Microsoft.Extensions.Localization;

    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected IStringLocalizer StringLocalizer { get; set; }
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                SetProperty(ref _title, value);
                if (!string.IsNullOrEmpty(_title))
                {
                    Analytics.TrackEvent(_title);
                }
            }
        }

        public ViewModelBase(INavigationService navigationService, IStringLocalizer stringLocalizer)
        {
            NavigationService = navigationService;
            StringLocalizer = stringLocalizer;
            
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
