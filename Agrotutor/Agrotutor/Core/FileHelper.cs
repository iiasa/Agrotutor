using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using SQLite;

namespace Agrotutor.Core
{
    public static class FileHelper
    {
        public static async Task<string> ReadTextAsync(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream(path);

            using (var file = new StreamReader(stream ?? throw new InvalidOperationException()))
            {
                return await file.ReadToEndAsync();
            }
        }
    }
}