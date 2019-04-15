using System;
using System.Collections.Generic;
using Agrotutor.Core.Entities;
using Agrotutor.Modules.Benchmarking.Types;
using Agrotutor.Modules.Ciat.Types;
using Agrotutor.Modules.PriceForecasting.Types;
using Agrotutor.Modules.Weather.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Agrotutor.Core.Persistence
{
    public class AppDataContext : DbContext, IAppDataContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<BemData> BemDatasets { get; set; }

        public DbSet<Cost> CostDatasets { get; set; }

        public DbSet<Income> IncomeDatasets { get; set; }

        public DbSet<Plot> Plots { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<DelineationPosition> Delineations { get; set; }

        public DbSet<Profit> ProfitDatasets { get; set; }

        public DbSet<Yield> YieldDatasets { get; set; }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public DbSet<WeatherHistory> WeatherHistories { get; set; }

        public DbSet<BenchmarkingInformation> BenchmarkingInformation { get; set; }

        public DbSet<PriceForecast> PriceForecasts { get; set; }

        public DbSet<CiatData> CiatData { get; set; }

        public DbSet<MediaItem> MediaItems { get; set; }

        public DbSet<CiatData.CiatDataDetail> CiatDataDetail { get; set; }

        public void DisableDetectChanges()
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public void EnableDetectChanges()
        {
            ChangeTracker.AutoDetectChangesEnabled = true;
            ChangeTracker.DetectChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var splitStringConverter =
                new ValueConverter<IEnumerable<string>, string>(v => string.Join(";", v), v => v.Split(';'));
            modelBuilder.Entity<CiatData.CiatDataDetail>()
                .Property(nameof(Modules.Ciat.Types.CiatData.CiatDataDetail.OptimalCultivars))
                .HasConversion(splitStringConverter);
            modelBuilder.Entity<CiatData.CiatDataDetail>()
                .Property(nameof(Modules.Ciat.Types.CiatData.CiatDataDetail.SuboptimalCultivars))
                .HasConversion(splitStringConverter);
            modelBuilder
                .Entity<Plot>()
                .Property(e => e.CropType)
                .HasConversion(
                    v => v.ToString(),
                    v => (CropType) Enum.Parse(typeof(CropType), v));
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Plot>().OwnsOne(s => s.Position);
            modelBuilder.Entity<DelineationPosition>().OwnsOne(s => s.Position);
        }
    }
}