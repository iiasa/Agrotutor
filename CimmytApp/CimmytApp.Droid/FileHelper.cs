using CimmytApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace CimmytApp.Droid
{
    using System;
    using System.IO;
    using SqLite.Contract;
    using SQLite.Net;
    using SQLite.Net.Platform.XamarinAndroid;

    public class FileHelper : IFileHelper
    {
        public SQLiteConnection GetConnection()
        {
            string filename = "Cimmyt.db3";
            string documentspath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentspath, filename);

            SQLitePlatformAndroid platform = new SQLitePlatformAndroid();
            SQLiteConnection connection = new SQLiteConnection(platform, path);

            return connection;
        }
    }
}