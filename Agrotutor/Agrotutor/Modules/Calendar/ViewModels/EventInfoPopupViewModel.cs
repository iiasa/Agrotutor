using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Agrotutor.Core;
using Agrotutor.Core.Entities;
using Agrotutor.Core.Persistence;
using Agrotutor.Modules.Calendar.Types;
using Microsoft.Extensions.Localization;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Agrotutor.Modules.Calendar.ViewModels
{
   public class EventInfoPopupViewModel : BindableBase, INavigationAware
    {
       private CalendarEvent _calenderEvent;
        private string _cropName;
        private string _cost;
        private string _plotName;
        private string _activityName;
        private bool _isActivityInfoShown;
        public IAppDataService AppDataService { get; set; }
        public INavigationService NavigationService { get; set; }
        public  EventInfoPopupViewModel(IAppDataService appDataService,INavigationService navigationService)
        {
            DeleteActivityCommand=new DelegateCommand(async () =>
            {
                await DeleteActivity();
            });
            AppDataService = appDataService;
            NavigationService = navigationService;
        }

        private async Task DeleteActivity()
        {
            if (_calenderEvent?.Data != null)
            {
             var res=  await AppDataService.RemovePlotActivityAsync(_calenderEvent?.Data);
                if (res)
                {
                    IsActivityInfoShown = false;
                }
            }
        }

        public string CropName
        {
            get => _cropName;
            set => SetProperty(ref this._cropName, value);
        }
        public string PlotName
        {
            get => _plotName;
            set => SetProperty(ref this._plotName, value);
        }
        public string ActivityName
        {
            get => _activityName;
            set => SetProperty(ref this._activityName, value);
        }
        public bool IsActivityInfoShown
        {
            get => _isActivityInfoShown;
            set => SetProperty(ref this._isActivityInfoShown, value);
        }
        public string Cost
        {
            get => _cost;
            set => SetProperty(ref this._cost, value);
        }
        public CalendarEvent CalenderEvent
       {
           get => _calenderEvent;
           set => SetProperty(ref this._calenderEvent, value);
        }

        public DelegateCommand DeleteActivityCommand { get; set; }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
           
        }

        public  void OnNavigatedTo(INavigationParameters parameters)
        {


            if (parameters.ContainsKey("event"))
            {
                parameters.TryGetValue("event",
                    out _calenderEvent);
                if (_calenderEvent != null)
                {
                    CropName = _calenderEvent.Plot.CropType.ToString();
                    PlotName = _calenderEvent.Plot.Name;
                    if(_calenderEvent.Data!=null&& _calenderEvent.Data.ActivityType != ActivityType.Initialization)
                    {
                        IsActivityInfoShown = true;
                        ActivityName = _calenderEvent.Data.ActivityType.ToString();
                        Cost = _calenderEvent.Data.Cost.ToString();
                    }
                    else
                    {
                        IsActivityInfoShown = false;
                    }
                }

            }
       
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
   
        }
    }
}
