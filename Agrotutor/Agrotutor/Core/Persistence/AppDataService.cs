namespace Agrotutor.Core.Persistence
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using Entities;
    using System.Linq;

    public class AppDataService : IAppDataService
    {
        public AppDataService(IAppDataContext context)
        {
            Context = context;
        }
        protected IAppDataContext Context { get; set; }

        public void DisableDetectChanges()
        {
            Context.DisableDetectChanges();
        }

        public void EnableDetectChanges()
        {
            Context.EnableDetectChanges();
        }

        public async Task AddPlot(Plot plot)
        {

            if (Context.Plots.Where(x => x.ID == plot.ID).Count() > 0)
            {
                await UpdatePlot(plot);
            }
            else
            {
                await Context.Plots.AddAsync(plot);
            }
            await Context.SaveChangesAsync();
        }

        public async Task UpdatePlot(Plot plot)
        {
            Context.Plots.Update(plot);
            await Context.SaveChangesAsync();
        }

        public async Task RemovePlot(Plot plot)
        {
            Context.Plots.Remove(plot);
            await Context.SaveChangesAsync();
        }

        public async Task<Plot> GetPlot(int id)
        {
            return await Context.Plots.FindAsync(id);
        }

        public async Task<IEnumerable<Plot>> GetAllPlots()
        {
            return await Context.Plots.ToListAsync();
        }
    }
}
