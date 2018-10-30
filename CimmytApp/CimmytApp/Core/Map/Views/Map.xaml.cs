namespace CimmytApp.Core.Map.Views
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
            this._bindingContext = (MapViewModel)BindingContext;
            this._bindingContext.SetViewReference(this);
        }

        public CameraPosition MapCenter
        {
            get => this._mapCenter;

            set
            {
                this._mapCenter = value;
                this.map.MoveCamera(CameraUpdateFactory.NewCameraPosition(value));
            }
        }

        private List<Pin> MapPins { get; set; }

        private List<Polygon> MapPolygons { get; set; }

        public void AddMapPin(Pin pin)
        {
            this.map.Pins.Add(pin);
            MapPins.Add(pin);
        }

        public void AddMapPolygon(Polygon polygon)
        {
            if (polygon.Positions.Count > 2)
            {
                this.map.Polygons.Add(polygon);
            }
            MapPolygons.Add(polygon);
        }

        public void ClearMapPins()
        {
            this.map.Pins.Clear();
            MapPins = new List<Pin>();
        }

        public void ClearMapPolygons()
        {
            this.map.Polygons.Clear();
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
            this.map.Pins.Clear();
            foreach (Pin pin in pins)
            {
                this.map.Pins.Add(pin);
            }
        }

        public void SetMapPolygons(List<Polygon> polygons)
        {
            MapPolygons = polygons;
            this.map.Polygons.Clear();
            foreach (var polygon in polygons.Where(polygon => polygon.Positions.Count > 2))
            {
                this.map.Polygons.Add(polygon);
            }
        }

        internal void MoveCamera(CameraUpdate mapCenter)
        {
            this.map.MoveCamera(mapCenter);
        }

        private void Map_OnAppearing(object sender, EventArgs e)
        {
            this._bindingContext.OnAppearing();
        }

        private void Map_OnDisappearing(object sender, EventArgs e)
        {
            this._bindingContext?.OnDisappearing();
        }

        private void OnMapClicked(object sender, EventArgs e)
        {
            this._bindingContext.MapClickedCommand.Execute(((MapClickedEventArgs)e).Point);
        }
    }
}