namespace CimmytApp.SQLiteDB
{
	using System.Collections.Generic;
    using CimmytApp.BusinessContract;
    using CimmytApp.DTO.Parcel;
    using SqLite.Contract;
	using SQLite.Net;
	using SQLiteNetExtensions.Extensions;
	using Xamarin.Forms;

    public class CimmytDbOperations : ICimmytDbOperations
	{
		readonly SQLiteConnection _databaseConn;

        public CimmytDbOperations()
        {

			_databaseConn = DependencyService.Get<IFileHelper>().GetConnection();
			_databaseConn.CreateTable<Parcel>();
        }

        public void AddParcel(Parcel parcel)
		{
			_databaseConn.InsertWithChildren(parcel, true);
        }

        public List<Parcel> GetAllParcels()
		{
			var parcels = new List<Parcel>();
			return _databaseConn.GetAllWithChildren<Parcel>();
        }
    }
}