using System;
using Agrotutor.Core.Tile;
using Agrotutor.Core.Tile.Entities;
using Foundation;
using MapKit;

namespace Agrotutor.iOS.UserInterface
{
    public class CustomTileProvider : MKTileOverlay
	{
		public CustomTileProvider(IReadOnlyTileService tileService)
		{
			TileService = tileService;
		}

		public IReadOnlyTileService TileService { get; set; }

		public override void LoadTileAtPath(MKTileOverlayPath path, MKTileOverlayLoadTileCompletionHandler result)
		{
			if (TileService != null)
			{
				Tile tile = TileService.TryGetTile((int)path.X, (int)((int)Math.Pow(2, path.Z) - 1 - path.Y), (int)path.Z);
				if (tile != null)
				{
					NSData tileData = NSData.FromArray(tile.TileData);
					result.Invoke(tileData, null);
				}
			}
		}
	}
}