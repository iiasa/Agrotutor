namespace CimmytApp.Core.Persistence
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CimmytApp.Core.Persistence.Entities;

    public interface IAppDataService
    {
        void DisableDetectChanges();

        void EnableDetectChanges();

        Task AddPlot(Plot plot);

        Task<IEnumerable<Plot>> GetAllPlots();
    }
}