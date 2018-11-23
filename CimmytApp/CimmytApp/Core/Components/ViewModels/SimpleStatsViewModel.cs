namespace CimmytApp.Core.Components.ViewModels
{
    using CimmytApp.ViewModels;

    using Microsoft.Extensions.Localization;

    public class SimpleStatsViewModel : ViewModelBase
    {
        public SimpleStatsViewModel(IStringLocalizer<SimpleStatsViewModel> stringLocalizer)
            : base(stringLocalizer)
        {
        }
    }
}
