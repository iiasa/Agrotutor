namespace CimmytApp.Core.Parcel.ViewModels
{
    using System.Collections.Generic;
    using CimmytApp.Core.DTO.Parcel;
    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.DTO.Parcel;
    using CimmytApp.ViewModels;
    using Microsoft.Extensions.Localization;
    using Prism.Navigation;

    public class ViewActivitiesPageViewModel : ViewModelBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;

        private List<Activity> _activities;

        public ViewActivitiesPageViewModel(INavigationService navigationService,
            IStringLocalizer<ViewActivitiesPageViewModel> localizer) : base(localizer)
        {
            _navigationService = navigationService;
        }

        public List<Activity> Activities
        {
            get => _activities;
            set => SetProperty(ref _activities, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Activities"))
            {
                parameters.TryGetValue<List<Activity>>("Activities", out var activities);
                if (activities != null)
                    Activities = activities;
            }
            else
            {
                _navigationService.GoBackAsync();
            }
        }
    }
}