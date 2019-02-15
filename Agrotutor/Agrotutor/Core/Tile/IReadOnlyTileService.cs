// <copyright file="IReadOnlyTileService.cs" company="IIASA">
// Copyright (c) IIASA. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Agrotutor.Core.Tile
{
    public interface IReadOnlyTileService
	{
		Agrotutor.Core.Tile.Entities.Tile TryGetTile(int tileColumn, int tileRow, int zoomLevel);
	}
}