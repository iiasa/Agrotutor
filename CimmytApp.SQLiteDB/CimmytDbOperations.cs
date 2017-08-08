using System;

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
        private readonly SQLiteConnection _databaseConn;

        public CimmytDbOperations()
        {
            _databaseConn = DependencyService.Get<IFileHelper>().GetConnection();

            _databaseConn.CreateTable<Parcel>();
            _databaseConn.CreateTable<PesticideApplication>();
        }

        public void AddParcel(Parcel parcel)
        {
            _databaseConn.InsertWithChildren(parcel, true);
        }

        public void DeleteParcel(Parcel parcel)
        {
            _databaseConn.Delete<Parcel>(parcel.ParcelId);
        }

        public List<Parcel> GetAllParcels()
        {
            return _databaseConn.GetAllWithChildren<Parcel>();
        }

        public Parcel GetParcelById(int parcelId)
        {
            return _databaseConn.GetWithChildren<Parcel>(parcelId);
        }

        public void UpdateParcel(Parcel parcel)
        {
            _databaseConn.UpdateWithChildren(parcel);
        }

        public int DeleteAllData()
        {
            return _databaseConn.DeleteAll<Parcel>();
        }
    }
}