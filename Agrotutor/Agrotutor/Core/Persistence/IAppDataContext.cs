using Agrotutor.Modules.Benchmarking.Types;
using Agrotutor.Modules.Ciat.Types;
using Agrotutor.Modules.PriceForecasting.Types;
using Agrotutor.Modules.Weather.Types;

namespace Agrotutor.Core.Persistence
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using Entities;

    public interface IAppDataContext
    {
         DbSet<Activity> Activities { get; set; }

         DbSet<BemData> BemDatasets { get; set; }

         DbSet<Cost> CostDatasets { get; set; }

         DbSet<Income> IncomeDatasets { get; set; }

         DbSet<Plot> Plots { get; set; }

        //public DbSet<Position> Positions { get; set; }

         DbSet<DelineationPosition> Delineations { get; set; }

         DbSet<Profit> ProfitDatasets { get; set; }

         DbSet<Yield> YieldDatasets { get; set; }

         DbSet<WeatherForecast> WeatherForecasts { get; set; }

         DbSet<WeatherHistory> WeatherHistories { get; set; }

         DbSet<BenchmarkingInformation> BenchmarkingInformation { get; set; }

         DbSet<PriceForecast> PriceForecasts { get; set; }

         DbSet<CiatData> CiatData { get; set; }

         DbSet<MediaItem> MediaItems { get; set; }

         DbSet<CiatData.CiatDataDetail> CiatDataDetail { get; set; }

        void DisableDetectChanges();

        void EnableDetectChanges();

        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}
