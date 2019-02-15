// <copyright file="ReadOnlyTileService.cs" company="IIASA">
// Copyright (c) IIASA. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Linq;
using Microsoft.Data.Sqlite;

namespace Agrotutor.Core.Tile
{
    public class ReadOnlyTileService : IReadOnlyTileService
	{
		public ReadOnlyTileService(TileContext tileContext)
		{
			TileContext = tileContext;
		}
        protected TileContext TileContext { get; set; }

		public Agrotutor.Core.Tile.Entities.Tile TryGetTile(int tileColumn, int tileRow, int zoomLevel)
		{
			lock (TileContext)
			{
				Agrotutor.Core.Tile.Entities.Tile returnedTile;
				try
				{
					returnedTile = TileContext.Tiles.SingleOrDefault(tile =>
						tile.ZoomLevel == zoomLevel && tile.TileColumn == tileColumn && tile.TileRow == tileRow);
				}
				catch (SqliteException ex)
				{
					returnedTile = null;
				}

				return returnedTile;
			}
		}
	}
}