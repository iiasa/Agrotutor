namespace CimmytApp.Core.Persistence
{
    using CimmytApp.Core.Persistence.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AppDataContext : DbContext, IAppDataContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }

        public DbSet<Plot> Plots { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Activity> Activities { get; set; }

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