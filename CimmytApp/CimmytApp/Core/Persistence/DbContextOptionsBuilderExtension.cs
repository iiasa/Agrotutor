namespace CimmytApp.Core.Persistence
{
    using System;
    using System.IO;
    using Microsoft.EntityFrameworkCore;
    using Xamarin.Forms;

    public static class DbContextOptionsBuilderExtension
    {

        public static void Configure<TContext>(this DbContextOptionsBuilder<TContext> dbContextOptionsBuilder,
            string filename = "localdata.db") where TContext : DbContext
        {
            string path = Path.Combine(GetPlatformFolder(), filename);
            dbContextOptionsBuilder.UseSqlite($"Filename={path}").UseLazyLoadingProxies();
        }

        private static string GetPlatformFolder()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library");

                case Device.Android:
                    return Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                default:
                    throw new NotSupportedException();
            }
        }
    }
}