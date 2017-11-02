﻿namespace CimmytApp.Parcel.ViewModels
{
    using System.Collections.Generic;
    using Prism.Mvvm;
    using Prism.Navigation;

    using DTO.Parcel;

    /// <inheritdoc cref="BindableBase" />
    /// <summary>
    /// Defines the <see cref="T:CimmytApp.Parcel.ViewModels.ViewActivitiesPageViewModel" />
    /// </summary>
    public class ViewActivitiesPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// Defines the _activities
        /// </summary>
        private List<AgriculturalActivity> _activities;

        /// <summary>
        /// Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Gets or sets the Activities
        /// </summary>
        public List<AgriculturalActivity> Activities { get => _activities; set => SetProperty(ref _activities, value); }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewActivitiesPageViewModel"/> class.
        /// </summary>
        public ViewActivitiesPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        /// <summary>
        /// The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="T:Prism.Navigation.NavigationParameters" /></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Activities"))
            {
                parameters.TryGetValue("Activities", out var activites);
                if (activites != null)
                {
                    Activities = (List<AgriculturalActivity>)activites;
                }
            }
            else
            {
                _navigationService.GoBackAsync();
            }
        }
    }
}