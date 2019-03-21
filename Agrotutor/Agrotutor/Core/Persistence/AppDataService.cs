using System;

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

        public async Task AddPlotAsync(Plot plot)
        {
            try
            {
                if (Context.Plots.Any(x => x.ID == plot.ID))
                {
                    await UpdatePlotAsync(plot);
                }
                else
                {
                    await Context.Plots.AddAsync(plot);
                }
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task UpdatePlotAsync(Plot plot)
        {
            Context.Plots.Update(plot);
            await Context.SaveChangesAsync();
        }

        public async Task RemovePlotAsync(Plot plot)
        {
            Context.Plots.Remove(plot);
            await Context.SaveChangesAsync();
        }

        public async Task<Plot> GetPlotAsync(int id)
        {
            return await Context.Plots.FindAsync(id);
        }

        public async Task<IEnumerable<Plot>> GetAllPlotsAsync()
        {
  
            var plots = await Context.Plots.ToListAsync() ?? new List<Plot>();
            return plots;
        }
    }
}
