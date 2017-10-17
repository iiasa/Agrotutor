using CimmytApp.BusinessContract;
using Helper.BusinessContract;
using Helper.DatasetSyncEvents.ViewModelBase;
using Prism;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace CimmytApp.ViewModels
{
	public class ParcelMainPageViewModel : DatasetSyncBindableBase, INavigationAware, IActiveAware, INotifyPropertyChanged
	{
		private DTO.Parcel.Parcel _parcel;
		private readonly INavigationService _navigationService;
		private readonly ICimmytDbOperations _cimmytDbOperations;
		public ICommand OverviewCommand { get; set; }
		public ICommand BenchmarkingCommand { get; set; }
		public ICommand CalenderCommand { get; set; }
		public ICommand WeatherCommand { get; set; }
		public ICommand MapCommand { get; set; }

		public DTO.Parcel.Parcel Parcel
		{
			get => _parcel;
			set
			{
				SetProperty(ref _parcel, value);
				OnPropertyChanged("Parcel");
				if (value != null)
					PublishDataset(value);
			}
		}

		public ParcelMainPageViewModel(INavigationService navigationService, ICimmytDbOperations cimmytDbOperations, IEventAggregator eventAggregator) : base(eventAggregator)
		{
			_navigationService = navigationService;
			OverviewCommand = new DelegateCommand(NavigateToOverview);
			BenchmarkingCommand = new DelegateCommand(NavigateToBenchMark);
			CalenderCommand = new DelegateCommand(NavigateToCalender);
			WeatherCommand = new DelegateCommand(NavigateToWeather);
			MapCommand = new DelegateCommand(NavigateToMap);
			
			try
            {
                _cimmytDbOperations = cimmytDbOperations;
            }
            catch (Exception e)
            {
            }
		}

		private void NavigateToOverview()
		{
			_navigationService.NavigateAsync("ViewParcelInformationPage");
		}

		private void NavigateToBenchMark()
		{
			_navigationService.NavigateAsync("LocalBenchmarkingSelectionPage");
		}

		private void NavigateToMap()
		{
			_navigationService.NavigateAsync("GenericMap");
		}

		private void NavigateToCalender()
		{
			_navigationService.NavigateAsync("TelerikCalendarPage");
		}

		private void NavigateToWeather()
		{
			_navigationService.NavigateAsync("WeatherDataSelection");
		}

		protected override IDataset GetDataset()
		{
			return Parcel;
		}

		protected override void ReadDataset(IDataset dataset)
		{
			Parcel = (DTO.Parcel.Parcel)dataset;
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
			
		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			try
			{
				var id = (int)parameters["Id"];

				Parcel = _cimmytDbOperations.GetParcelById(id);
			}
			catch (Exception e)
			{
			}
		}

		public bool IsActive { get; set; }
		public event EventHandler IsActiveChanged;
	}
}
