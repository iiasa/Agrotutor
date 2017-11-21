namespace Helper.Map.Views
{
    using System;
    using Helper.Map.ViewModels;
    using TK.CustomMap;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

    /// <inheritdoc />
    /// <summary>
    ///     Defines the <see cref="T:Helper.Map.Views.GenericMap" />
    /// </summary>
    public partial class GenericMap : ContentPage
    {
        /// <summary>
        ///     Defines the contextObj
        /// </summary>
        private readonly GenericMapViewModel _contextObj;

        /// <summary>
        ///     Defines the map
        /// </summary>
        private TKCustomMap _map;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericMap" /> class.
        /// </summary>
        public GenericMap()
        {
            InitializeComponent();
            _contextObj = (GenericMapViewModel)BindingContext;
        }

        /// <summary>
        ///     The GenericMap_OnAppearing
        /// </summary>
        /// <param name="sender">The <see cref="object" /></param>
        /// <param name="e">The <see cref="EventArgs" /></param>
        private void GenericMap_OnAppearing(object sender, EventArgs e)
        {
            if (Map?.Children?.Count != 0 || _contextObj == null)
            {
                return;
            }

            _contextObj.OnAppearing();

            _map = new TKCustomMap();

            _map.SetBinding(TKCustomMap.CustomPinsProperty, "ViewPins");
            _map.SetBinding(TKCustomMap.PolygonsProperty, "ViewPolygons");
            _map.SetBinding(TKCustomMap.MapRegionProperty, "MapRegion");
            _map.SetBinding(TKCustomMap.MapCenterProperty, "MapsPosition");
            _map.SetBinding(TKCustomMap.MapClickedCommandProperty, "MapClickedCommand");
            _map.SetBinding(TKCustomMap.MapLongPressCommandProperty, "MapLongPressCommand");
            _map.SetBinding(Map.MapTypeProperty, "MapType");

            _map.HasZoomEnabled = true;
            _map.MapType = MapType.Hybrid;
            _map.IsShowingUser = false;
            _map.MapRegion = GenericMapViewModel.InitialMapRegion;

            Map.Children.Add(_map);
        }

        /// <summary>
        ///     The GenericMap_OnDisappearing
        /// </summary>
        /// <param name="sender">The <see cref="object" /></param>
        /// <param name="e">The <see cref="EventArgs" /></param>
        private void GenericMap_OnDisappearing(object sender, EventArgs e)
        {
            Map?.Children?.Clear();
            _contextObj?.OnDisappearing();
        }
    }
}