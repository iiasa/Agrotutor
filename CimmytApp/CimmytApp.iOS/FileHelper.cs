using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SqLite.Contract;
using SQLite.Net;
using Xamarin.Forms;
using CimmytApp.iOS;

[assembly: Dependency(typeof(FileHelper))]
namespace CimmytApp.iOS
{
    public class FileHelper : IFileHelper
    {
        public SQLiteConnection GetConnection()
        {
            var fileName = "Cimmyt.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);

            var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            var connection = new SQLite.Net.SQLiteConnection(platform, path);

            return connection;
        }
    }
}