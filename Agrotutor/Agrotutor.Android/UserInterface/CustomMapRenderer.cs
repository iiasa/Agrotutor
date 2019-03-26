// <copyright file="CustomMapRenderer.cs" company="IIASA">
// Copyright (c) IIASA. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel;
using Agrotutor.Core.Tile;
using Agrotutor.Droid.UserInterface;
using Agrotutor.UserInterface.CustomMap;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace Agrotutor.Droid.UserInterface
{
    public class CustomMapRenderer : MapRenderer, IUpdatable
    {
        public CustomMapRenderer(Context context)
            : base(context)
        {
            // Store the current Map Renderer into LayerService
            LayerService.MapRenderer = this;
        }

        protected CustomMap CustomMap => Element as CustomMap;

        protected bool IsInitialized { get; set; }

        protected TileOverlay TileOverlay { get; set; }

        public void Update(string mbtilesFileName)
        {
            SetMbTilesAsBackground(mbtilesFileName);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);


            if (CustomMap.ShowTileLayerProperty.PropertyName == e.PropertyName)
                if (TileOverlay != null)
                    TileOverlay.Visible = CustomMap.ShowTileLayer;
            if (CustomMap.ShowSatelliteTileLayerProperty.PropertyName == e.PropertyName)

                CustomMap.MapType = CustomMap.ShowSatelliteTileLayer ? MapType.Hybrid : MapType.None;
        }

        protected override void OnMapReady(GoogleMap nativeMap, Map map)
        {
            if (IsInitialized) return;

            if (nativeMap != null)
            {
                base.OnMapReady(nativeMap, map);
                CustomMap.UiSettings.CompassEnabled = true;
                nativeMap.UiSettings.MyLocationButtonEnabled = true;
                nativeMap.UiSettings.RotateGesturesEnabled = true;
                nativeMap.UiSettings.ZoomControlsEnabled = true;
                nativeMap.UiSettings.TiltGesturesEnabled = true;
                nativeMap.UiSettings.ZoomGesturesEnabled = true;
                nativeMap.UiSettings.MapToolbarEnabled = true;
                LayerService.CurrentMap = nativeMap;
                CustomMap.MapType = CustomMap.ShowSatelliteTileLayer ? MapType.Hybrid : MapType.None;
                //try
                //{
                //    // Find a better way to get dependencies
                //    var options = new TileOverlayOptions().InvokeZIndex(0f)
                //        .InvokeTileProvider(new CustomTileProvider(
                //            (IReadOnlyTileService) ((App) Application.Current).Container.Resolve(
                //                typeof(IReadOnlyTileService))));

                //    TileOverlay = nativeMap.AddTileOverlay(options);
                //    TileOverlay.Visible = CustomMap.ShowTileLayer;


                //}
                //catch (Exception ex)
                //{
                //    Console.Write(ex.Message);
                //}

                IsInitialized = true;
            }
        }

        public void SetMbTilesAsBackground(string mbtilesFileName)
        {
            // Remove previous TileOverlay if created
            if (TileOverlay != null) return;

            // Make Db Context Options Builder to create sqlite db builder
            var myContextBuilder = new DbContextOptionsBuilder<TileContext>();
            //myContextBuilder.UseSqlite($"Filename={mbtilesFileName}");

            // Initialize TileContext with this builder
            var tileContext = new TileContext(myContextBuilder.Options);

            // Create TileService with this TileConext
            var readOnlyTileService = new ReadOnlyTileService(tileContext);

            // Create CustomTileProvider with this TileService
            var customTileProvider = new CustomTileProvider(readOnlyTileService);

            // And finally, create the TileOverlayOptions
            var options = new TileOverlayOptions().InvokeZIndex(0f)
                .InvokeTileProvider(customTileProvider);

            // And TileOverlay
            var map = (GoogleMap) LayerService.CurrentMap;
            TileOverlay = map.AddTileOverlay(options);
        }
    }
}