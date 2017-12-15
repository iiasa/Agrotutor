namespace Helper.Map.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Helper.Map.ViewModels;
    using Xamarin.Forms.GoogleMaps;

    public partial class Map
    {
        private readonly MapViewModel _bindingContext;

        private CameraPosition _mapCenter;

        public Map()
        {
            InitializeComponent();
            _bindingContext = (MapViewModel)BindingContext;
            _bindingContext.SetViewReference(this);
        }

        public CameraPosition MapCenter
        {
            get => _mapCenter;

            set
            {
                _mapCenter = value;
                map.MoveCamera(CameraUpdateFactory.NewCameraPosition(value));
            }
        }

        private List<Pin> MapPins { get; set; }

        private List<Polygon> MapPolygons { get; set; }

        public void AddMapPin(Pin pin)
        {
            map.Pins.Add(pin);
            MapPins.Add(pin);
        }

        public void AddMapPolygon(Polygon polygon)
        {
            if (polygon.Positions.Count > 2)
            {
                map.Polygons.Add(polygon);
            }
            MapPolygons.Add(polygon);
        }

        public void ClearMapPins()
        {
            map.Pins.Clear();
            MapPins = new List<Pin>();
        }

        public void ClearMapPolygons()
        {
            map.Polygons.Clear();
            MapPolygons = new List<Polygon>();
        }

        public List<Pin> GetMapPins()
        {
            return MapPins;
        }

        public List<Polygon> GetMapPolygons()
        {
            return MapPolygons;
        }

        public void SetMapPins(List<Pin> pins)
        {
            MapPins = pins;
            map.Pins.Clear();
            foreach (var pin in pins)
            {
                map.Pins.Add(pin);
            }
        }

        public void SetMapPolygons(List<Polygon> polygons)
        {
            MapPolygons = polygons;
            map.Polygons.Clear();
            foreach (var polygon in polygons.Where(polygon => polygon.Positions.Count > 2))
            {
                map.Polygons.Add(polygon);
            }
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
    }
}