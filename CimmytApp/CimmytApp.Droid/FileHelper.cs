
using System;
using System.IO;
using SqLite.Contract;
using Xamarin.Forms;
using SQLite.Net;
using CimmytApp.Droid;

[assembly: Dependency(typeof(FileHelper))]
namespace CimmytApp.Droid
{
    public class FileHelper : IFileHelper
    {
        public SQLiteConnection GetConnection()
        {
            var filename = "Cimmyt.db3";
            var documentspath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentspath, filename);

            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var connection = new SQLite.Net.SQLiteConnection(platform, path);
            return connection;

        }
    }
}