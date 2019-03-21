// <copyright file="LayerService.cs" company="IIASA">
// Copyright (c) IIASA. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace Agrotutor.Core.Tile
{
    public static class LayerService
	{
		/// <summary>
		/// Constante for the points layer.
		/// </summary>
		public const int LAYERPOINTS = 1;
		public const int LAYERGOOGLEMAP = 2;

		public static IUpdatable MapRenderer { get; set; } = null;

		public static object CurrentMap { get; set; } = null;

		/// <summary>
		/// Gets or sets list of layer loaded.
		/// </summary>
		public static ICollection<LayerItem> LayerItems { get; set; } = new List<LayerItem>();

		/// <summary>
		/// Get layer by id (return the layer with the specified id, or null if not found).
		/// </summary>
		/// <param name="id">Id of the layer to search.</param>
		/// <returns>layer or null.</returns>
		public static LayerItem GetLayerById(int id)
		{
			foreach (var item in LayerItems)
			{
				if (item.Id == id)
				{
					return item;
				}
			}

			return null;
		}

		/// <summary>
		/// Get layer by name (return the first layer with the specified name, or null if not found).
		/// </summary>
		/// <param name="name">Name of the layer to search.</param>
		/// <returns>layer or null.</returns>
		public static LayerItem GetLayerByName(string name)
		{
			foreach (var item in LayerItems)
			{
				if (item.Name == name)
				{
					return item;
				}
			}

			return null;
		}

		/// <summary>
		/// Add a layer if not exists.
		/// </summary>
		/// <param name="name">Name of layer to add.</param>
		/// <param name="isEnabled">Set if layer can be activate or not.</param>
		/// <returns>void.</returns>
		public static LayerItem AddLayerPoints(string name, bool isEnabled)
		{
		    LayerItem currentItem = new LayerItem
            {
				Id = LAYERPOINTS,
				IsChecked = isEnabled,
				Name = name,
				Icon = "ic_pin_white_24dp",
				IsEnabled = isEnabled,
			};
			LayerItems.Add(currentItem);

			return currentItem;
		}

		public static LayerItem AddLayerRaster(string name, bool isEnabled, bool isChecked, string fileName)
		{
		    LayerItem currentItem = new LayerItem
            {
				Id = GetMaxId() + 1,
				IsChecked = isChecked,
				Name = name,
				Icon = "ic_layers_white_24dp",
				IsEnabled = isEnabled,
				FileName = fileName,
			};
			LayerItems.Add(currentItem);
			return currentItem;
		}

		/// <summary>
		/// Reset the list of layers.
		/// </summary>
		public static void Reset()
		{
			LayerItems.Clear();
		}

        /// <summary>
        /// Update the visibility of a layer.
        /// </summary>
        /// <param name="mbtilesFileName">name of layer.</param>
        public static void UpdateIsChecked( string mbtilesFileName)
        {
            MapRenderer?.Update( mbtilesFileName);
        }

		/// <summary>
		/// Return the current raster layer Id selected.
		/// </summary>
		/// <returns>Id of the current raster layer selected.</returns>
		public static int GetCurrentOfflineRasterId()
		{
			foreach (LayerItem lay in LayerItems)
			{
				if ((lay.Id != LAYERPOINTS) && (lay.Id != LAYERGOOGLEMAP))
				{
					if (lay.IsChecked)
					{
						return lay.Id;
					}
				}
			}

			return -1;
		}

		/// <summary>
		/// Get the visiblity of a layer by its Id.
		/// </summary>
		/// <param name="id">Id of layer.</param>
		/// <returns>Visibility.</returns>
		public static bool GetIsCheckedById(int id)
		{
		    LayerItem layer = GetLayerById(id);
			if (layer != null)
			{
				return layer.IsChecked;
			}

			return false;
		}

		public static bool IsGoogleMap(int rasterId)
		{
			return rasterId == 2;
		}

		public static bool IsGoogleMapChecked()
		{
		    LayerItem layer = GetLayerById(2);
			return layer.IsChecked;
		}

		public static string GetMBTileFileName(int rasterId)
		{
		    LayerItem layer = GetLayerById(rasterId);
			if (layer != null)
			{
				return layer.FileName;
			}

			return null;
		}

		private static int GetMaxId()
		{
			int max = -1;
			foreach (LayerItem layer in LayerService.LayerItems)
			{
				if (layer.Id > max)
				{
					max = layer.Id;
				}
			}

			return max;
		}
	}
}