using CimmytApp.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace CimmytApp.UWP
{
    using System.IO;
    using Windows.Storage;
    using SqLite.Contract;
    using SQLite.Net;
    using SQLite.Net.Platform.WinRT;

    public class FileHelper : IFileHelper
    {
        public SQLiteConnection GetConnection()
        {
            string filename = "Cimmyt.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);

            SQLitePlatformWinRT platfrom = new SQLitePlatformWinRT();
            SQLiteConnection connection = new SQLiteConnection(platfrom, path);
            return connection;
        }
    }
}