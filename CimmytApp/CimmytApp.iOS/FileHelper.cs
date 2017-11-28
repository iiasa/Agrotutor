using CimmytApp.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace CimmytApp.iOS
{
    using System;
    using System.IO;
    using SqLite.Contract;
    using SQLite.Net;
    using SQLite.Net.Platform.XamarinIOS;

    public class FileHelper : IFileHelper
    {
        public SQLiteConnection GetConnection()
        {
            string fileName = "Cimmyt.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library");
            string path = Path.Combine(libraryPath, fileName);

            SQLitePlatformIOS platform = new SQLitePlatformIOS();
            SQLiteConnection connection = new SQLiteConnection(platform, path);

            return connection;
        }
    }
}