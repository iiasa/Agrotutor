namespace CimmytApp.Parcel.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;
    using System.Collections.Generic;

    using DTO.Parcel;
    using System;

    /// <summary>
    /// Defines the <see cref="ActivityPageViewModel" />
    /// </summary>
    public class ActivityPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Defines the CallingDetailPage
        /// </summary>
        private bool CallingDetailPage = false;

        /// <summary>
        /// Defines the _isActive
        /// </summary>
        private bool _isActive;

        private Parcel _parcel;
        private string _caller = "ParcelPage";

        /// <summary>
        /// Gets or sets the ActivityClickedCommand
        /// </summary>
        public DelegateCommand<string> ActivityClickedCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService"/></param>
        public ActivityPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ActivityClickedCommand = new DelegateCommand<string>(ExecuteMethod);
            SaveCommand = new DelegateCommand(Save);
        }

        private void Save()
        {
            var parameters = new NavigationParameters
            {
                { "Activities", Activities },
                { "Parcel", Parcel }
            };
            _navigationService.NavigateAsync($"app:///{_caller}", parameters);
        }

        /// <summary>
        /// The ExecuteMethod
        /// </summary>
        /// <param name="activityType">The <see cref="string"/></param>
        private void ExecuteMethod(string activityType)
        {
            CallingDetailPage = true;
            _navigationService.NavigateAsync("ActivityDetail", new NavigationParameters() { { "activityType", activityType } });
        }

        /// <summary>
        /// The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            CallingDetailPage = false;
            if (parameters.ContainsKey("Activity"))
            {
                parameters.TryGetValue("Activity", out var activity);
                if (Activities == null) Activities = new List<AgriculturalActivity>();
                Activities.Add((AgriculturalActivity)activity);
            }
            if (parameters.ContainsKey("Activities"))
            {
                parameters.TryGetValue("Activities", out var activities);
                Activities = (List<AgriculturalActivity>)activities;
            }
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue("Parcel", out var parcel);
                Parcel = (Parcel)parcel;
            }
            if (parameters.ContainsKey("Caller")){
                parameters.TryGetValue("Caller", out var caller );
                _caller = (string)caller;
            }
        }

        /// <summary>
        /// Gets or sets the Activities
        /// </summary>
        public List<AgriculturalActivity> Activities { get; set; }

        public DelegateCommand SaveCommand { get; private set; }

        public Parcel Parcel
        {
            get => _parcel;
            private set => SetProperty(ref _parcel, value);
        }
    }
}