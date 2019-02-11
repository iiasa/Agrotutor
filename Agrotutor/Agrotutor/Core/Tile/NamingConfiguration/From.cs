// <copyright file="From.cs" company="IIASA">
// Copyright (c) IIASA. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Agrotutor.Core.Tile.NamingConfiguration
{
    public static class From
	{
		public static Func<IMutableEntityType, string> ClrType => entity => entity.ClrType.Name;

		public static Func<IMutableEntityType, string> DbSet => entity => entity.Relational().TableName;
	}
}