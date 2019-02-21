// <copyright file="CustomMap.cs" company="IIASA">
// Copyright (c) IIASA. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Agrotutor.UserInterface.CustomMap
{
    public class CustomMap : Map
    {
        public static readonly BindableProperty ShowTileLayerProperty =
            BindableProperty.Create(nameof(ShowTileLayer), typeof(bool), typeof(CustomMap), true);
        public static readonly BindableProperty ShowSatelliteTileLayerProperty =
            BindableProperty.Create(nameof(ShowSatelliteTileLayer), typeof(bool), typeof(CustomMap), true);
        public bool ShowTileLayer
        {
            get => (bool) GetValue(ShowTileLayerProperty);
            set => SetValue(ShowTileLayerProperty, value);
        }
        public bool ShowSatelliteTileLayer
        {
            get => (bool)GetValue(ShowSatelliteTileLayerProperty);
            set => SetValue(ShowSatelliteTileLayerProperty, value);
        }
    }
}