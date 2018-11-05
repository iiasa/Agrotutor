namespace CimmytApp.Core.Parcel.ViewModels
{
    using System.Collections.Generic;
    using CimmytApp.Core.DTO.Parcel;
    using CimmytApp.DTO.Parcel;
    using CimmytApp.ViewModels;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class ActivityPageViewModel : ViewModelBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private string _caller = "ParcelPage";
        private Parcel _parcel;
        private bool CallingDetailPage;

        public ActivityPageViewModel(INavigationService navigationService,
            IStringLocalizer<ActivityPageViewModel> localizer) : base(localizer)
        {
            _navigationService = navigationService;
            ActivityClickedCommand = new DelegateCommand<string>(ExecuteMethod);
            SaveCommand = new DelegateCommand(Save);
        }

        public DelegateCommand SaveCommand { get; }
        public List<AgriculturalActivity> Activities { get; set; }
        public DelegateCommand<string> ActivityClickedCommand { get; set; }

        public Parcel Parcel
        {
            get => _parcel;
            private set => SetProperty(ref _parcel, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            CallingDetailPage = false;
            if (parameters.ContainsKey("Activity"))
            {
                parameters.TryGetValue<AgriculturalActivity>("Activity", out var activity);
                if (Activities == null)
                {
                    Activities = new List<AgriculturalActivity>();
                }
                Activities.Add(activity);
            }
            if (parameters.ContainsKey("Activities"))
            {
                parameters.TryGetValue<List<AgriculturalActivity>>("Activities", out var activities);
                Activities = activities;
            }
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue<Parcel>("Parcel", out var parcel);
                Parcel = parcel;
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
                { "Parcel", Parcel },
                { "EditEnabled", true }
            };
            _navigationService.NavigateAsync($"app:///{_caller}", parameters);
        }
    }
}