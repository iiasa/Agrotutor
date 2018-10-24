namespace CimmytApp.Core.Parcel.ViewModels
{
    using CimmytApp.ViewModels;
    using Microsoft.Extensions.Localization;

    public class DeleteActivityPageViewModel : ViewModelBase
    {
        public DeleteActivityPageViewModel(IStringLocalizer<DeleteActivityPageViewModel> localizer) 
            : base(localizer)
        {
        }
    }
}