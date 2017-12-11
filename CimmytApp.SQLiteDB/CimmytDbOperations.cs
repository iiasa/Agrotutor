namespace CimmytApp.SQLiteDB
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using CimmytApp.BusinessContract;
    using CimmytApp.DTO.BEM;
    using Helper.Realm;
    using Helper.Realm.DTO;
    using Realms;

    public class CimmytDbOperations : ICimmytDbOperations
    {
        private Realm _realm;
        private Realm Realm
        {
            get
            {
                if (_realm == null)
                {
                    try
                    {
                        _realm = DbContext.GetConnection();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message, e);
                    }
                }
                return _realm;
            }
        }

        public void AddParcel(ParcelDTO parcel)
        {
            var parcels = GetAllParcels();
            var max = 0;
            foreach (var p in parcels){
                if (p.ParcelId > max) max = p.ParcelId;
            }
            max += 1;
            parcel.ParcelId = max;
            Realm.Write(() => Realm.Add(parcel));
        }

        public void DeleteParcel(ParcelDTO parcel)
        {

            Realm.Remove(parcel);
        }

        public List<ParcelDTO> GetAllParcels()
        {
            return Realm.All<ParcelDTO>().ToList();

        }

        public BemData GetBemData()
        {
            return new BemData
            {
                Costo = Realm.All<Costo>().ToList(),
                Ingreso = Realm.All<Ingreso>().ToList(),
                Rendimiento = Realm.All<Rendimiento>().ToList(),
                Utilidad = Realm.All<Utilidad>().ToList()
            };
        }

        public ParcelDTO GetParcelById(int parcelId)
        {
            return Realm.All<ParcelDTO>().Where(p => p.ParcelId == parcelId).FirstOrDefault(); // TODO check if null works...
        }

        public void SaveCostos(List<Costo> listCostos)
        {
            Realm.Write(() =>Realm.RemoveAll<Costo>());
            foreach (var costo in listCostos){
                Realm.Write(() =>Realm.Add(costo));
            }
        }

        public void SaveIngresos(List<Ingreso> listIngresos)
        {
            Realm.Write(() => Realm.RemoveAll<Ingreso>());
            foreach (var ingreso in listIngresos)
            {
                Realm.Write(() => Realm.Add(ingreso));
            }
        }

        public void SaveRendimientos(List<Rendimiento> listRendimientos)
        {
            Realm.Write(() => Realm.RemoveAll<Rendimiento>());
            foreach (var rendimiento in listRendimientos)
            {
                Realm.Write(() => Realm.Add(rendimiento));
            }
        }

        public void SaveUtilidades(List<Utilidad> listUtilidades)
        {
            Realm.Write(() => Realm.RemoveAll<Utilidad>());
            foreach (var utilidad in listUtilidades)
            {
                Realm.Write(() => Realm.Add(utilidad));
            }
        }

        public void UpdateParcel(ParcelDTO parcel)
        {
            Realm.Write(() => Realm.Add(parcel, update: true));
        }
    }
}