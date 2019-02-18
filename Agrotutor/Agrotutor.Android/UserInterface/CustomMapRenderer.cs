// <copyright file="CustomMapRenderer.cs" company="IIASA">
// Copyright (c) IIASA. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.ComponentModel;
using Agrotutor.Core.Tile;
using Agrotutor.Droid.UserInterface;
using Agrotutor.UserInterface.CustomMap;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace Agrotutor.Droid.UserInterface
{
    public class CustomMapRenderer : MapRenderer
    {
        public CustomMapRenderer(Context context)
            : base(context)
        {
        }

        protected CustomMap CustomMap => Element as CustomMap;

        protected bool IsInitialized { get; set; }

        protected TileOverlay TileOverlay { get; set; }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);


            if (CustomMap.ShowTileLayerProperty.PropertyName == e.PropertyName)
                if (TileOverlay != null)
                    TileOverlay.Visible = CustomMap.ShowTileLayer;
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
                try
                {
                    // Find a better way to get dependencies
                    var options = new TileOverlayOptions().InvokeZIndex(0f)
                        .InvokeTileProvider(new CustomTileProvider(
                            (IReadOnlyTileService) ((App) Application.Current).Container.Resolve(
                                typeof(IReadOnlyTileService))));

                    TileOverlay = nativeMap.AddTileOverlay(options);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }

                IsInitialized = true;
            }
        }
    }
}