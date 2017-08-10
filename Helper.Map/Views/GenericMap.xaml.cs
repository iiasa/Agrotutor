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
            contextObj = (GenericMapViewModel)BindingContext;
        }

        private void GenericMap_OnDisappearing(object sender, EventArgs e)
        {
            MapLayout.Children.Clear();
        }

        private void GenericMap_OnAppearing(object sender, EventArgs e)
        {
            TKCustomMap map = new TKCustomMap();
            contextObj.GetPosition();
            map.CustomPins = contextObj.CustomPinsList;
            map.Polygons = contextObj.MapPolygons;

            map.MapClickedCommand = contextObj.MapClickedCommand;
            map.MapLongPressCommand = contextObj.MapLongPressCommand;

            MapLayout.Children.Add(map);
        }
    }
}