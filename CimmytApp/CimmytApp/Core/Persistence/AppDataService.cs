﻿namespace CimmytApp.Core.Persistence
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CimmytApp.Core.Persistence.Entities;
    using Microsoft.EntityFrameworkCore;

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
            await Context.Plots.AddAsync(plot);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Plot>> GetAllPlots()
        {
            return await Context.Plots.ToListAsync();
        }
    }
}