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
		public DelegateCommand<string> NavigateAsyncCommand { get; set; }

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
			NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);

			try
            {
                _cimmytDbOperations = cimmytDbOperations;
            }
            catch (Exception e)
            {
            }
		}

		private void NavigateAsync(string page)
		{
			_navigationService.NavigateAsync(page);
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
