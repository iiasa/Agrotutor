using System.Linq;

namespace Helper.Map.Views
{
    using System;
    using TK.CustomMap;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

    using ViewModels;

    /// <summary>
    /// Defines the <see cref="GenericMap" />
    /// </summary>
    public partial class GenericMap : ContentPage
    {
        /// <summary>
        /// Defines the contextObj
        /// </summary>
        private GenericMapViewModel contextObj;

        /// <summary>
        /// Defines the map
        /// </summary>
        private TKCustomMap map;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericMap"/> class.
        /// </summary>
        public GenericMap()
        {
            InitializeComponent();
            contextObj = (GenericMapViewModel)BindingContext;
        }

        //private void TestMethod(string s)
        //{
        //    if(map!=null)
        //    map.MoveToRegion(contextObj.MapRegion);
        //}
        /// <summary>
        /// The GenericMap_OnDisappearing
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="EventArgs"/></param>
        private void GenericMap_OnDisappearing(object sender, EventArgs e)
        {
            MapLayout?.Children?.Clear();
            contextObj?.OnDisappearing();
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
                if (contextObj != null)
                {
                    contextObj.OnAppearing();
                    contextObj.DrawPolygonsOnMap();
                    contextObj.AdjustMapZoom();

                    map = new TKCustomMap();

                    map.SetBinding(TKCustomMap.CustomPinsProperty, "CustomPinsList");
                    map.SetBinding(TKCustomMap.PolygonsProperty, "MapPolygons");
                    map.SetBinding(TKCustomMap.MapRegionProperty, "MapRegion");
                    map.SetBinding(TKCustomMap.MapCenterProperty, "MapsPosition");
                    map.SetBinding(TKCustomMap.MapClickedCommandProperty, "MapClickedCommand");
                    map.SetBinding(TKCustomMap.MapLongPressCommandProperty, "MapLongPressCommand");

                    map.HasZoomEnabled = true;
                    map.MapType = MapType.Hybrid;
                    map.IsShowingUser = false;

                    MapLayout.Children.Add(map);
                }
            }
        }
    }
}