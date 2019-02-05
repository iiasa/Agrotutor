namespace Agrotutor.Core.Persistence
{
    using Microsoft.EntityFrameworkCore;

    using Entities;
    using Agrotutor.Modules.Weather.Types;
    using Agrotutor.Modules.Benchmarking.Types;
    using Agrotutor.Modules.PriceForecasting.Types;

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

        public DbSet<Profit> ProfitDatasets { get; set; }

        public DbSet<Yield> YieldDatasets { get; set; }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public DbSet<WeatherHistory> WeatherHistories { get; set; }

        public DbSet<BenchmarkingInformation> BenchmarkingInformation { get; set; }

        public DbSet<PriceForecast> PriceForecasts { get; set; }

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
            base.OnModelCreating(modelBuilder);
        }
    }
}
