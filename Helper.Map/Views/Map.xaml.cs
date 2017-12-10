using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Helper.Map.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Helper.Map.Views
{
    public partial class Map : ContentPage
    {
        private MapViewModel _bindingContext;

        public List<Pin> MapPins { get; set; }
        public List<Polygon> MapPolygons { get; set; }

        CameraPosition mapCenter;

        public CameraPosition MapCenter
        {
            get
            {
                return mapCenter;
            }

            set
            {
                mapCenter = value;
                map.MoveCamera(CameraUpdateFactory.NewCameraPosition(value));
            }
        }


        public Map()
        {
            InitializeComponent();
            _bindingContext = (MapViewModel)BindingContext;
            _bindingContext.SetViewReference(this);
        }

        internal void MoveCamera(CameraUpdate mapCenter)
        {
            map.MoveCamera(mapCenter);
        }
    }
}
