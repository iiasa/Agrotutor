namespace Helper.Map.Views
{
    using System;
    using System.Collections.Generic;
    using Helper.Map.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;

    public partial class Map : ContentPage
    {
        private MapViewModel _bindingContext;

        private List<Pin> MapPins { get; set; }
        private List<Polygon> MapPolygons { get; set; }

        private CameraPosition mapCenter;

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

        private void Map_OnAppearing(object sender, EventArgs e)
        {
            _bindingContext.OnAppearing();
        }

        private void Map_OnDisappearing(object sender, EventArgs e)
        {
            _bindingContext?.OnDisappearing();
        }

        private void OnMapClicked(object sender, EventArgs e)
        {
            _bindingContext.MapClickedCommand.Execute(((MapClickedEventArgs)e).Point);
        }

        public void SetMapPins(List<Pin> pins)
        {
            MapPins = pins;
            map.Pins.Clear();
            foreach (Pin pin in pins)
            {
                map.Pins.Add(pin);
            }
        }

        public void ClearMapPins()
        {
            map.Pins.Clear();
            MapPins = new List<Pin>();
        }

        public void AddMapPin(Pin pin)
        {
            map.Pins.Add(pin);
            MapPins.Add(pin);
        }

        public List<Pin> GetMapPins()
        {
            return MapPins;
        }

        public void SetMapPolygons(List<Polygon> polygons)
        {
            MapPolygons = polygons;
            map.Polygons.Clear();
            foreach (Polygon polygon in polygons)
                if (polygon.Positions.Count > 2)
                    map.Polygons.Add(polygon);
        }

        public void ClearMapPolygons()
        {
            map.Polygons.Clear();
            MapPolygons = new List<Polygon>();
        }

        public void AddMapPolygon(Polygon polygon)
        {
            if (polygon.Positions.Count > 2) map.Polygons.Add(polygon);
            MapPolygons.Add(polygon);
        }

        public List<Polygon> GetMapPolygons()
        {
            return MapPolygons;
        }
    }
}