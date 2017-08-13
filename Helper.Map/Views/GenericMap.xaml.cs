using System;
using System.Threading.Tasks;
using CimmytApp.DTO.Parcel;
using Helper.Map.ViewModels;
using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Helper.Map.Views
{
    public partial class GenericMap : ContentPage
    {
        private GenericMapViewModel contextObj;
        private TKCustomMap map;
        public GenericMap()
        {
            InitializeComponent();
            contextObj = (GenericMapViewModel)BindingContext;
         //   contextObj._eventAggregator.GetEvent<Test>().Subscribe(TestMethod);



        }

        //private void TestMethod(string s)
        //{
        //    if(map!=null)
        //    map.MoveToRegion(contextObj.MapRegion);
        //}

        private void GenericMap_OnDisappearing(object sender, EventArgs e)
        {
            MapLayout.Children.Clear();
        }

        private void GenericMap_OnAppearing(object sender, EventArgs e)
        {
            if (MapLayout.Children.Count == 0)
            {
                 map = new TKCustomMap();
                // map.BindingContext = contextObj;

                map.SetBinding(TKCustomMap.CustomPinsProperty, "CustomPinsList");
                map.SetBinding(TKCustomMap.PolygonsProperty, "MapPolygons");
                map.SetBinding(TKCustomMap.MapRegionProperty, "MapRegion");
                map.SetBinding(TKCustomMap.MapCenterProperty, "MapsPosition");
                map.SetBinding(TKCustomMap.MapClickedCommandProperty, "MapClickedCommand");
                map.SetBinding(TKCustomMap.MapLongPressCommandProperty, "MapLongPressCommand");

                map.HasZoomEnabled = true;
                map.MapType = MapType.Street;
                map.IsShowingUser = false;

                //map.MapClickedCommand = contextObj.MapClickedCommand;
                //map.MapLongPressCommand = contextObj.MapLongPressCommand;

                MapLayout.Children.Add(map);
            }

            //TKCustomMap map = new TKCustomMap();
            // contextObj.GetPosition();
            //map.CustomPins = contextObj.CustomPinsList;

            //MapLayout.Children.Add(map);
        }
    }
}