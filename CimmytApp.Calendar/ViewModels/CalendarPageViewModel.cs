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
        private DateTime? _selectCurrentDate;
        private List<DateTime> _selectedDatesList;

        private List<SpecialDate> _specialDatesList;

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

        public DelegateCommand<object> DateClickCommand { get; set; }

        public DateTime? SelectCurrentDate
        {
            get => _selectCurrentDate;
            set => SetProperty(ref _selectCurrentDate, value);
        }

        public List<DateTime> SelectedDatesList
        {
            get => _selectedDatesList;
            set => SetProperty(ref _selectedDatesList, value);
        }

        public List<SpecialDate> SpecialDatesList
        {
            get => _specialDatesList;
            set => SetProperty(ref _specialDatesList, value);
        }

        private void DateClieckCommandAction(object obj)
        {
            DateTime? x = SelectCurrentDate;
        }
    }
}