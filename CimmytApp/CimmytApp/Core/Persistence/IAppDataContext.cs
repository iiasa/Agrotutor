namespace CimmytApp.Core.Persistence
{
    using System.Threading;
    using System.Threading.Tasks;
    using CimmytApp.Core.Persistence.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IAppDataContext
    {
        DbSet<Plot> Plots { get; }
        DbSet<Position> Positions { get; }


        void DisableDetectChanges();

        void EnableDetectChanges();

        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
    }
}