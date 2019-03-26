// <copyright file="TileContext.cs" company="IIASA">
// Copyright (c) IIASA. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Agrotutor.Core.Tile.Entities;
using Agrotutor.Core.Tile.NamingConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Agrotutor.Core.Tile
{
    public class TileContext : DbContext
    {
        public TileContext(DbContextOptions<TileContext> options)
            : base(options)
        {
        }

        public DbSet<Metadata> Metadata { get; set; }

        public DbSet<Entities.Tile> Tiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.Tile>()
                .HasKey(x => new
                {
                    x.ZoomLevel,
                    x.TileColumn,
                    x.TileRow
                });

            //modelBuilder.ConfigureNames(NamingOptions.Default.SetNamingScheme(NamingScheme.SnakeCase)
            //    .SetTableNamingSource(From.DbSet));
        }
    }
}