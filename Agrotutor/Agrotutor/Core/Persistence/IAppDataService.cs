namespace Agrotutor.Core.Persistence
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Entities;

    public interface IAppDataService
    {
        void DisableDetectChanges();

        void EnableDetectChanges();

        Task AddPlot(Plot plot);

        Task UpdatePlot(Plot plot);

        Task RemovePlot(Plot plot);

        Task<IEnumerable<Plot>> GetAllPlots();

        Task<Plot> GetPlot(int id);
    }
}
