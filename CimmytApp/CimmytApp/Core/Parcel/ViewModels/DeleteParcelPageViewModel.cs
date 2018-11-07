namespace CimmytApp.Core.Parcel.ViewModels
{
    using System;
    using CimmytApp.Core.Persistence;
    using CimmytApp.Core.Persistence.Entities;
    using CimmytApp.ViewModels;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Navigation;

    /// <summary>
    ///     Defines the <see cref="DeleteParcelPageViewModel" />
    /// </summary>
    public class DeleteParcelPageViewModel : ViewModelBase, INavigatedAware
    {

        private readonly INavigationService _navigationService;

        private Plot _plot;

        public IAppDataService AppDataService { get; set; }

        public DeleteParcelPageViewModel(INavigationService navigationService, 
            IAppDataService appdataService, IStringLocalizer<DeleteParcelPageViewModel> localizer)
        : base(localizer)
        {
            _navigationService = navigationService;
            AppDataService = appdataService;
            Delete = new DelegateCommand(DeleteParcel);
            GoBack = new DelegateCommand(Back);
        }

        public DelegateCommand Delete { get; set; }

        public DelegateCommand GoBack { get; set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Plot"))
            {
                this._plot = (Plot)parameters["Plot"];
            }
            else
            {
                try
                {
                    AppDataService.GetPlot((int)parameters["Id"]);
                }
                catch (Exception)
                {
                    Back();
                }
            }
        }

        private void Back()
        {
            _navigationService.GoBackAsync();
        }

        private void DeleteParcel()
        {
            AppDataService.RemovePlot(_plot);
            _navigationService.NavigateAsync("app:///MainPage");
        }
    }
}