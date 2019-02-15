// <copyright file="CustomMapRenderer.cs" company="IIASA">
// Copyright (c) IIASA. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel;
using Agrotutor.Core.Tile;
using Agrotutor.iOS.UserInterface;
using Agrotutor.UserInterface.CustomMap;
using MapKit;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace Agrotutor.iOS.UserInterface
{
    public class CustomMapRenderer : MapRenderer
    {
        private MKMapView map;

        protected CustomMap CustomMap => Element as CustomMap;

        protected MKTileOverlay TileOverlay { get; set; }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                map = Control as MKMapView;
                TileOverlay = new CustomTileProvider(
                    (IReadOnlyTileService) ((App) Xamarin.Forms.Application.Current).Container.Resolve(
                        typeof(IReadOnlyTileService)));
                if (map != null)
                {
                    map.AddOverlay(TileOverlay);

                    map.OverlayRenderer = OverlayRenderer;
                }

            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (CustomMap.ShowTileLayerProperty.PropertyName == e.PropertyName)
            {
                if (map == null) return;

                if (CustomMap.ShowTileLayer)
                    map.AddOverlay(TileOverlay, MKOverlayLevel.AboveRoads);
                else
                    map.RemoveOverlay(TileOverlay);
            }
        }


        protected MKOverlayRenderer OverlayRenderer(MKMapView mapview, IMKOverlay overlay)
        {
            return new MKTileOverlayRenderer((MKTileOverlay) overlay);
        }
    }
}