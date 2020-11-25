using System;

namespace Agrotutor.Core.Persistence
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using Entities;
    using System.Linq;
    using Microsoft.AppCenter.Crashes;

    public class AppDataService : IAppDataService
    {
        public AppDataService(IAppDataContext context) => Context = context;
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
                Crashes.TrackError(e);
            }
        }

        public async Task UpdatePlotAsync(Plot plot)
        {
            Context.Plots.Update(plot);
            await Context.SaveChangesAsync();
        }
        public async Task<bool> RemovePlotActivityAsync(Activity activity)
        {
            bool result = false;
            try
            {
                Context.Activities.Remove(activity);
                result = await Context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }

            return result;
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
            List<Plot> plots = null;
            try{
                plots = await Context.Plots.Include(x => x.Activities).Include(x => x.Delineation)
                            .Include(x => x.MediaItems).Include(x => x.PriceForecast)
                            .Include(x => x.BemData).ThenInclude(x => x.Cost)
                            .Include(x => x.BemData).ThenInclude(x => x.Income)
                            .Include(x => x.BemData).ThenInclude(x => x.Profit)
                            .Include(x => x.BemData).ThenInclude(x => x.Yield)
                            .Include(x => x.BenchmarkingInformation).Include(x => x.CiatData)
                            .Include(x => x.WeatherForecast).Include(x => x.WeatherHistory).ToListAsync() ??
                        new List<Plot>();
            } catch (Exception e)
            {
                Crashes.TrackError(e);
                plots = new List<Plot>();
            }
            return plots;
        }
    }
}
