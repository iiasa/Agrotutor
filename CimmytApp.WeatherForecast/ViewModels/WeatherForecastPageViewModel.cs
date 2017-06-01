using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CimmytApp.DTO;
using Xamarin.Forms;

namespace CimmytApp.WeatherForecast.ViewModels
{
    public class WeatherForecastPageViewModel : BindableBase
    {
        private Parcel _parcel;

        public Parcel Parcel
        {
            get { return _parcel; }
            set { SetProperty(ref _parcel, value); }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    var changed = PropertyChanged;
        //    if (changed != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}

        //private static void HandleParcelChanged(BindableObject bindable, object oldvalue, object newvalue)
        //{
        //    var i = 0;
        //    i++;
        //}

        public ICommand TestCommand { get; set; }

        public WeatherForecastPageViewModel()
        {
            TestCommand = new Command(Test);
        }

        public void Test()
        {
            var i = 0;
            i++;
        }
    }
}