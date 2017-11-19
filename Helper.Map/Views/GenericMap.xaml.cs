namespace Helper.Map.Views
{
    using System;
    using TK.CustomMap;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

    /// <inheritdoc />
    /// <summary>
    /// Defines the <see cref="T:Helper.Map.Views.GenericMap" />
    /// </summary>
    public partial class GenericMap : ContentPage
    {
        /// <summary>
        /// Defines the contextObj
        /// </summary>
        private readonly GenericMapViewModel _contextObj;

        /// <summary>
        /// Defines the map
        /// </summary>
        private TKCustomMap _map;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericMap"/> class.
        /// </summary>
        public GenericMap()
        {
            InitializeComponent();
            _contextObj = (GenericMapViewModel)BindingContext;
        }

        /// <summary>
        /// The GenericMap_OnDisappearing
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void GenericMap_OnDisappearing(object sender, EventArgs e)
        {
            MapLayout?.Children?.Clear();
            _contextObj?.OnDisappearing();
        }

        /// <summary>
        /// The GenericMap_OnAppearing
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void GenericMap_OnAppearing(object sender, EventArgs e)
        {
            if (MapLayout?.Children?.Count == 0)
            {
                if (_contextObj != null)
                {
                    _contextObj.OnAppearing();
                    _contextObj.DrawPolygonsOnMap();
                    _contextObj.AdjustMapZoom();

                    _map = new TKCustomMap();

                    _map.SetBinding(TKCustomMap.CustomPinsProperty, "CustomPinsList");
                    _map.SetBinding(TKCustomMap.PolygonsProperty, "MapPolygons");
                    _map.SetBinding(TKCustomMap.MapRegionProperty, "MapRegion");
                    _map.SetBinding(TKCustomMap.MapCenterProperty, "MapsPosition");
                    _map.SetBinding(TKCustomMap.MapClickedCommandProperty, "MapClickedCommand");
                    _map.SetBinding(TKCustomMap.MapLongPressCommandProperty, "MapLongPressCommand");
                    _map.SetBinding(TKCustomMap.MapTypeProperty, "MapType");

                    _map.HasZoomEnabled = true;
                    _map.MapType = MapType.Hybrid;
                    _map.IsShowingUser = false;

                    MapLayout.Children.Add(_map);
                }
            }
        }
    }
}