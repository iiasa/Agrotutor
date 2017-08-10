using System;
using System.Threading.Tasks;
using Helper.Map.ViewModels;
using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Helper.Map.Views
{


    public partial class GenericMap : ContentPage
    {
        private GenericMapViewModel contextObj;
        public GenericMap()
        {
            InitializeComponent();
            contextObj =  (GenericMapViewModel)BindingContext;
        }

        private void GenericMap_OnDisappearing(object sender, EventArgs e)
        {
           MapLayout.Children.Clear();
        }



        private void GenericMap_OnAppearing(object sender, EventArgs e)
        {

            if (MapLayout.Children.Count == 0)
            {
                TKCustomMap map = new TKCustomMap();
               // map.BindingContext = contextObj;

                map.SetBinding(TKCustomMap.CustomPinsProperty, "CustomPinsList");
                map.SetBinding(TKCustomMap.MapRegionProperty, "MapRegion");
                map.SetBinding(TKCustomMap.MapCenterProperty, "MapsPosition");
           
                map.HasZoomEnabled = true;
                map.MapType = MapType.Hybrid;
                map.IsShowingUser = false;
                
                MapLayout.Children.Add(map);
            }



            //TKCustomMap map = new TKCustomMap();
            // contextObj.GetPosition();
            //map.CustomPins = contextObj.CustomPinsList;

            //MapLayout.Children.Add(map);
        }
    }
}