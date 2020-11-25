using Prism.Navigation;

namespace Agrotutor.Core.Components.ViewModels
{
    using Microsoft.Extensions.Localization;

    public class SimpleStatsViewModel : ViewModelBase
    {
        public SimpleStatsViewModel(INavigationService navigationService, IStringLocalizer<SimpleStatsViewModel> stringLocalizer)
            : base(navigationService, stringLocalizer)
        {
            Title = "SimpleStats";
        }
    }
}
