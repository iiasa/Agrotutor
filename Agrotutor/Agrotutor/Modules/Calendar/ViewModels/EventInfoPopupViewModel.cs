using System;
using System.Collections.Generic;
using System.Text;
using Agrotutor.Core;
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
                    if(_calenderEvent.Data!=null)
                    {
                        ActivityName = _calenderEvent.Data.ActivityType.ToString();
                        Cost = _calenderEvent.Data.Cost.ToString();
                    }
                }

            }
       
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
   
        }
    }
}
