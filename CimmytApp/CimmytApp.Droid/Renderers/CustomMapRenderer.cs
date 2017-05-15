using CimmytApp.Droid.Renderers;
using CimmytApp.Map;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace CimmytApp.Droid.Renderers
{
    using Xamarin.Forms.Maps.Android;
    using Android.Gms.Maps;
    using Android.Gms.Maps.Model;

    public class CustomMapRenderer : MapRenderer
    {
        private MapView map;
        private CustomTileProvider tileProvider;
        private CustomMap customMap;

        //https://github.com/blounty/CustomMapTiles
        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Maps.Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                map = Control as MapView;
                customMap = e.NewElement as CustomMap;

                var tileProvider = new CustomTileProvider(512, 512, customMap.MapTileTemplate);
                var options = new TileOverlayOptions().InvokeTileProvider(tileProvider);

                //map.Map.AddTileOverlay(options);
            }
        }
    }
}