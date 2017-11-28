namespace CimmytApp.Parcel.ViewModels
{
    using System.Collections.Generic;
    using CimmytApp.DTO.Parcel;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    /// <summary>
    ///     Defines the <see cref="ActivityPageViewModel" />
    /// </summary>
    public class ActivityPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        ///     Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        private string _caller = "ParcelPage";

#pragma warning disable CS0169 // The field 'ActivityPageViewModel._isActive' is never used

        /// <summary>
        ///     Defines the _isActive
        /// </summary>
        private bool _isActive;

#pragma warning restore CS0169 // The field 'ActivityPageViewModel._isActive' is never used

        private Parcel _parcel;

#pragma warning disable CS0414 // The field 'ActivityPageViewModel.CallingDetailPage' is assigned but its value is never used

        /// <summary>
        ///     Defines the CallingDetailPage
        /// </summary>
        private bool CallingDetailPage;

#pragma warning restore CS0414 // The field 'ActivityPageViewModel.CallingDetailPage' is assigned but its value is never used

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivityPageViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService" /></param>
        public ActivityPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ActivityClickedCommand = new DelegateCommand<string>(ExecuteMethod);
            SaveCommand = new DelegateCommand(Save);
        }

        /// <summary>
        ///     Gets or sets the Activities
        /// </summary>
        public List<AgriculturalActivity> Activities { get; set; }

        /// <summary>
        ///     Gets or sets the ActivityClickedCommand
        /// </summary>
        public DelegateCommand<string> ActivityClickedCommand { get; set; }

        public Parcel Parcel
        {
            get => _parcel;
            private set => SetProperty(ref _parcel, value);
        }

        public DelegateCommand SaveCommand { get; private set; }

        /// <summary>
        ///     The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <summary>
        ///     The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters" /></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            CallingDetailPage = false;
            if (parameters.ContainsKey("Activity"))
            {
                parameters.TryGetValue("Activity", out object activity);
                if (Activities == null)
                {
                    Activities = new List<AgriculturalActivity>();
                }
                Activities.Add((AgriculturalActivity)activity);
            }
            if (parameters.ContainsKey("Activities"))
            {
                parameters.TryGetValue("Activities", out object activities);
                Activities = (List<AgriculturalActivity>)activities;
            }
            if (parameters.ContainsKey("Parcel"))
            {
                parameters.TryGetValue("Parcel", out object parcel);
                Parcel = (Parcel)parcel;
            }
            if (parameters.ContainsKey("Caller"))
            {
                parameters.TryGetValue("Caller", out object caller);
                _caller = (string)caller;
            }
        }

        /// <summary>
        ///     The ExecuteMethod
        /// </summary>
        /// <param name="activityType">The <see cref="string" /></param>
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
            NavigationParameters parameters = new NavigationParameters
            {
                { "Activities", Activities },
                { "Parcel", Parcel },
                { "EditEnabled", true }
            };
            _navigationService.NavigateAsync($"app:///{_caller}", parameters);
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}