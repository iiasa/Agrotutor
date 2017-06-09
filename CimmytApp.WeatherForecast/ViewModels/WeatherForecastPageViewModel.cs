namespace CimmytApp.WeatherForecast.ViewModels
{
    using Prism.Mvvm;
    using System.Windows.Input;
    using Xamarin.Forms;
    using DTO.Parcel;

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