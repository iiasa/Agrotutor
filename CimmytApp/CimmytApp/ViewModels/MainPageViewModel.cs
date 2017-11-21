namespace CimmytApp.ViewModels
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using CimmytApp.BusinessContract;
	using CimmytApp.DTO.Parcel;
	using CimmytApp.Parcel.Events;
	using Helper.Map.ViewModels;
	using Prism.Commands;
	using Prism.Events;
	using Prism.Mvvm;
	using Prism.Navigation;
	using TK.CustomMap;
	using TK.CustomMap.Overlays;
	using Xamarin.Forms;
	using Xamarin.Forms.Maps;

	/// <summary>
	///     Defines the <see cref="MainPageViewModel" />
	/// </summary>
	public class MainPageViewModel : BindableBase, INavigationAware
	{
		/// <summary>
		///     Defines the _cimmytDbOperations
		/// </summary>
		private readonly ICimmytDbOperations _cimmytDbOperations;

		/// <summary>
		///     Defines the _eventAggregator
		/// </summary>
		private readonly IEventAggregator _eventAggregator;

		/// <summary>
		///     Defines the _navigationService
		/// </summary>
		private readonly INavigationService _navigationService;

		/// <summary>
		///     Defines the _title
		/// </summary>
		private string _title;

		/// <summary>
		///     Initializes a new instance of the <see cref="MainPageViewModel" /> class.
		/// </summary>
		/// <param name="eventAggregator">The <see cref="IEventAggregator" /></param>
		/// <param name="navigationService">The <see cref="INavigationService" /></param>
		/// <param name="cimmytDbOperations">The <see cref="ICimmytDbOperations" /></param>
		public MainPageViewModel(IEventAggregator eventAggregator, INavigationService navigationService,
			ICimmytDbOperations cimmytDbOperations)
		{
			this._navigationService = navigationService;
			this._cimmytDbOperations = cimmytDbOperations;
			this._eventAggregator = eventAggregator;
			this._eventAggregator.GetEvent<DbConnectionRequestEvent>().Subscribe(OnDbConnectionRequest);
			this._eventAggregator.GetEvent<DbConnectionAvailableEvent>().Publish();

			NavigateAsyncCommand = new DelegateCommand<string>(NavigateAsync);
			NavigateToMapCommand = new DelegateCommand(NavigateToMap);
		}

		public MainPageViewModel()
		{
		}

		/// <summary>
		///     Gets or sets the NavigateAsyncCommand
		/// </summary>
		public DelegateCommand<string> NavigateAsyncCommand { get; set; }

		/// <summary>
		///     Gets or sets the NavigateToMapCommand
		/// </summary>
		public DelegateCommand NavigateToMapCommand { get; set; }

		/// <summary>
		///     Gets or sets the Title
		/// </summary>
		public string Title
		{
			get => this._title;
			set => SetProperty(ref this._title, value);
		}

		/// <summary>
		///     The OnNavigatedFrom
		/// </summary>
		/// <param name="parameters">The <see cref="NavigationParameters" /></param>
		public void OnNavigatedFrom(NavigationParameters parameters)
		{
		}

		/// <summary>
		///     The OnNavigatedTo
		/// </summary>
		/// <param name="parameters">The <see cref="NavigationParameters" /></param>
		public void OnNavigatedTo(NavigationParameters parameters)
		{
		}

		/// <summary>
		///     The OnNavigatingTo
		/// </summary>
		/// <param name="parameters">The <see cref="NavigationParameters" /></param>
		public void OnNavigatingTo(NavigationParameters parameters)
		{
		}

		/// <summary>
		///     The NavigateAsync
		/// </summary>
		/// <param name="page">The <see cref="string" /></param>
		private void NavigateAsync(string page)
		{
			this._navigationService.NavigateAsync(page);
		}

		/// <summary>
		///     The NavigateToMap
		/// </summary>
		private void NavigateToMap()
		{
			ObservableCollection<TKPolygon> polygons = new ObservableCollection<TKPolygon>();
			ObservableCollection<TKCustomMapPin> parcelLocations = new ObservableCollection<TKCustomMapPin>();
			List<Parcel> parcels = this._cimmytDbOperations.GetAllParcels();

			foreach (Parcel item in parcels)
			{
				if (item.Polygon != null)
				{
					TKPolygon polygon = new TKPolygon
					{
						StrokeColor = Color.Green,
						StrokeWidth = 2f,
						Color = Color.Red,
					};
					List<Position> listPosition = item.Polygon.ListPoints
						.Select(positionitem => new Position(positionitem.Latitude, positionitem.Longitude))
						.ToList();
					if (listPosition.Count <= 2)
					{
						continue;
					}

					polygon.Coordinates = listPosition;
					polygons.Add(polygon);
				}

				if (item.Latitude != 0 && item.Longitude != 0)
				{
					parcelLocations.Add(new TKCustomMapPin
					{
						Position = new Position(item.Latitude, item.Longitude)
					});
				}
			}

			NavigationParameters parameters = new NavigationParameters
			{
				{ GenericMapViewModel.PolygonsParameterName, polygons },
				{ GenericMapViewModel.PointsParameterName, parcelLocations }
			};

			this._navigationService.NavigateAsync("GenericMap", parameters);
		}

		/// <summary>
		///     The OnDbConnectionRequest
		/// </summary>
		private void OnDbConnectionRequest()
		{
			this._eventAggregator.GetEvent<DbConnectionEvent>().Publish(this._cimmytDbOperations);
		}
	}
}