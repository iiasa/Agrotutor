namespace CimmytApp.Calendar.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Prism.Commands;
    using Prism.Mvvm;
    using Xamarin.Forms;
    using XamForms.Controls;

    public class CalendarPageViewModel : BindableBase
    {
        private List<DateTime> _selectedDatesList;

        public List<DateTime> SelectedDatesList
        {
            get { return _selectedDatesList; }
            set { SetProperty(ref _selectedDatesList, value); }
        }

        private DateTime? _selectCurrentDate;
        private List<SpecialDate> _specialDatesList;

        public DateTime? SelectCurrentDate
        {
            get { return _selectCurrentDate; }
            set { SetProperty(ref _selectCurrentDate, value); }
        }

        public List<SpecialDate> SpecialDatesList
        {
            get { return _specialDatesList; }
            set { SetProperty(ref _specialDatesList, value); }
        }

        public DelegateCommand<object> DateClickCommand { get; set; }

        public CalendarPageViewModel()
        {
            DateClickCommand = new DelegateCommand<object>(DateClieckCommandAction);
            SelectedDatesList = new List<DateTime>();
            SpecialDatesList = new List<SpecialDate>
            {
                new SpecialDate(DateTime.Now.AddDays(2))
                {
                    BackgroundColor = Color.Green,
                    TextColor = Color.Accent,
                    BorderColor = Color.Lime,
                    BorderWidth = 8,
                    Selectable = true
                },
                new SpecialDate(DateTime.Now.AddDays(3))
                {
                    BackgroundColor = Color.Green,
                    TextColor = Color.Blue,
                    Selectable = true
                }
            };
        }

        private void DateClieckCommandAction(object obj)
        {
            var x = SelecteCurrentDate;
        }
    }
}