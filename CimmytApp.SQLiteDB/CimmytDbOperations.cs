using System;
using SQLiteNetExtensions.Extensions.TextBlob;

namespace CimmytApp.SQLiteDB
{
    using System.Collections.Generic;
    using SqLite.Contract;
    using SQLite.Net;
    using SQLiteNetExtensions.Extensions;
    using Xamarin.Forms;

    using BusinessContract;
    using DTO.BEM;
    using DTO.Parcel;

    public class CimmytDbOperations : ICimmytDbOperations
    {
        private readonly SQLiteConnection _databaseConn;

        public CimmytDbOperations()
        {
            try
            {

            _databaseConn = DependencyService.Get<IFileHelper>().GetConnection();
           
            _databaseConn.CreateTable<Parcel>();
            _databaseConn.CreateTable<PolygonDto>();
            _databaseConn.CreateTable<PesticideApplication>();
            _databaseConn.CreateTable<Costo>();
            _databaseConn.CreateTable<Ingreso>();
            _databaseConn.CreateTable<Rendimiento>();
            _databaseConn.CreateTable<Utilidad>();
                
            }
            catch (Exception)
            {
               
            }
        }

        public void AddParcel(Parcel parcel)
        {
            _databaseConn.InsertWithChildren(parcel, true);
        }

        public int DeleteAllData()
        {
            return _databaseConn.DeleteAll<Parcel>();
        }

        public void DeleteParcel(Parcel parcel)
        {
            _databaseConn.Delete<Parcel>(parcel.ParcelId);
        }

        public List<Parcel> GetAllParcels()
        {
            return _databaseConn.GetAllWithChildren<Parcel>();
        }

        public void SaveParcelPolygon(int parcelId, PolygonDto polygonObj)
        {
            if (polygonObj == null)
                throw new ArgumentNullException();

            var parcelObj = _databaseConn.GetWithChildren<Parcel>(parcelId);
          
            if (parcelObj != null)
            {
                if (parcelObj.Polygon == null)
                {
                    parcelObj.Polygon = polygonObj;
                    _databaseConn.InsertOrReplaceWithChildren(parcelObj);
                }
                else
                {
                    parcelObj.Polygon.ListPoints = polygonObj.ListPoints;
                    _databaseConn.UpdateWithChildren(parcelObj);

                }
               // _databaseConn.Delete<Parcel>(parcelObj.ParcelId);
              
              
               
            }


        }

        public BemData GetBemData()
        {
            return new BemData()
            {
                Costo = _databaseConn.GetAllWithChildren<Costo>(),
                Ingreso = _databaseConn.GetAllWithChildren<Ingreso>(),
                Rendimiento = _databaseConn.GetAllWithChildren<Rendimiento>(),
                Utilidad = _databaseConn.GetAllWithChildren<Utilidad>()
            };
        }

        public Parcel GetParcelById(int parcelId)
        {
            return _databaseConn.GetWithChildren<Parcel>(parcelId);
        }

        public void SaveCostos(List<Costo> listCostos)
        {
            _databaseConn.DeleteAll<Costo>();
            _databaseConn.InsertAll(listCostos);
        }

        public void SaveIngresos(List<Ingreso> listIngresos)
        {
            _databaseConn.DeleteAll<Ingreso>();
            _databaseConn.InsertAll(listIngresos);
        }

        public void SaveRendimientos(List<Rendimiento> listRendimientos)
        {
            _databaseConn.DeleteAll<Rendimiento>();
            _databaseConn.InsertAll(listRendimientos);
        }

        public void SaveUtilidades(List<Utilidad> listUtilidades)
        {
            _databaseConn.DeleteAll<Utilidad>();
            _databaseConn.InsertAll(listUtilidades);
        }

        public void UpdateParcel(Parcel parcel)
        {
            _databaseConn.UpdateWithChildren(parcel);
        }
    }
}