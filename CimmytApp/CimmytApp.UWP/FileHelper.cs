using System;
using System.Collections.Generic;
using System.IO;

using Windows.Storage;
using Xamarin.Forms;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using CimmytApp.UWP;
using SqLite.Contract;

[assembly: Dependency(typeof(FileHelper))]
namespace CimmytApp.UWP
{
    public class FileHelper : IFileHelper
    {
        public SQLiteConnection GetConnection()
        {
            var filename = "Cimmyt.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);

            var platfrom = new SQLitePlatformWinRT();
            var connection = new SQLite.Net.SQLiteConnection(platfrom, path);
            return connection;
        }
    }
}
