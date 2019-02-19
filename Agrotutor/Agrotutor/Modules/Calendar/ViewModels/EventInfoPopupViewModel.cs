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
        private string _title;
        private string _cost;

        public string Title
        {
            get => _title;
            set => SetProperty(ref this._title, value);
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
                    Title = _calenderEvent.Title;
                    Cost = _calenderEvent.Data.Cost.ToString();
                }

            }
       
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
   
        }
    }
}
