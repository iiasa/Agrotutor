namespace CimmytApp.Core.Parcel.ViewModels
{
    using System.Collections.Generic;
    using CimmytApp.Core.DTO.Parcel;
    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.ViewModels;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    public class ActivityPageViewModel : ViewModelBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private string _caller = "ParcelPage";
        private Plot _plot;
        private bool CallingDetailPage;

        public ActivityPageViewModel(INavigationService navigationService,
            IStringLocalizer<ActivityPageViewModel> localizer) : base(localizer)
        {
            _navigationService = navigationService;
            ActivityClickedCommand = new DelegateCommand<string>(ExecuteMethod);
            SaveCommand = new DelegateCommand(Save);
        }

        public DelegateCommand SaveCommand { get; }
        public List<Activity> Activities { get; set; }
        public DelegateCommand<string> ActivityClickedCommand { get; set; }

        public Plot Plot
        {
            get => this._plot;
            private set => SetProperty(ref this._plot, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            CallingDetailPage = false;
            if (parameters.ContainsKey("Activity"))
            {
                parameters.TryGetValue<Activity>("Activity", out var activity);
                if (Activities == null)
                {
                    Activities = new List<Activity>();
                }
                Activities.Add(activity);
            }
            if (parameters.ContainsKey("Activities"))
            {
                parameters.TryGetValue<List<Activity>>("Activities", out var activities);
                Activities = activities;
            }
            if (parameters.ContainsKey("Plot"))
            {
                parameters.TryGetValue<Plot>("Plot", out var plot);
                Plot = plot;
            }
            if (parameters.ContainsKey("Caller"))
            {
                parameters.TryGetValue<string>("Caller", out var caller);
                _caller = caller;
            }
        }

        private void ExecuteMethod(string activityType)
        {
            CallingDetailPage = true;
            _navigationService.NavigateAsync("ActivityDetail", new NavigationParameters
            {
                { "activityType", activityType }
            });
        }

        private void Save()
        {
            var parameters = new NavigationParameters
            {
                { "Activities", Activities },
                { "Plot", Plot },
                { "EditEnabled", true }
            };
            _navigationService.NavigateAsync($"app:///{_caller}", parameters);
        }
    }
}