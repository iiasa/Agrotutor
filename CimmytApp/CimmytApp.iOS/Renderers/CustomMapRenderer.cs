using CimmytApp.iOS.Renderers;
using CimmytApp.Map;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace CimmytApp.iOS.Renderers
{
    using Xamarin.Forms.Maps.iOS;
    using MapKit;

    public class CustomMapRenderer : MapRenderer
    {
        private MKMapView mapView;
        private CustomMap customMap;

        protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null) return;
            mapView = Control as MKMapView;
            customMap = e.NewElement as CustomMap;

            var overlay = new MKTileOverlay(customMap.MapTileTemplate)
            {
                CanReplaceMapContent = false,
                GeometryFlipped = false
            };

            if (mapView != null)
            {
                mapView.AddOverlay(overlay, MKOverlayLevel.AboveLabels);
                mapView.OverlayRenderer = (mv, o) =>
                    new MKTileOverlayRenderer((MKTileOverlay)o);
            }
        }
    }
}