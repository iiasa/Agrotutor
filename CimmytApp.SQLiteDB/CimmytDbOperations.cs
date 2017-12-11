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
        private readonly Realm _realm;

        public CimmytDbOperations()
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

        public void AddParcel(ParcelDTO parcel)
        {
            var parcels = GetAllParcels();
            var max = 0;
            foreach (var p in parcels){
                if (p.ParcelId > max) max = p.ParcelId;
            }
            max += 1;
            parcel.ParcelId = max;
            _realm.Write(() => _realm.Add(parcel));
        }

        public void DeleteParcel(ParcelDTO parcel)
        {

            _realm.Remove(parcel);
        }

        public List<ParcelDTO> GetAllParcels()
        {
            return _realm.All<ParcelDTO>().ToList();

        }

        public BemData GetBemData()
        {
            return new BemData
            {
                Costo = _realm.All<Costo>().ToList(),
                Ingreso = _realm.All<Ingreso>().ToList(),
                Rendimiento = _realm.All<Rendimiento>().ToList(),
                Utilidad = _realm.All<Utilidad>().ToList()
            };
        }

        public ParcelDTO GetParcelById(int parcelId)
        {
            return _realm.All<ParcelDTO>().Where(p => p.ParcelId == parcelId).FirstOrDefault(); // TODO check if null works...
        }

        public void SaveCostos(List<Costo> listCostos)
        {
            _realm.Write(() =>_realm.RemoveAll<Costo>());
            foreach (var costo in listCostos){
                _realm.Write(() =>_realm.Add(costo));
            }
        }

        public void SaveIngresos(List<Ingreso> listIngresos)
        {
            _realm.Write(() => _realm.RemoveAll<Ingreso>());
            foreach (var ingreso in listIngresos)
            {
                _realm.Write(() => _realm.Add(ingreso));
            }
        }

        public void SaveRendimientos(List<Rendimiento> listRendimientos)
        {
            _realm.Write(() => _realm.RemoveAll<Rendimiento>());
            foreach (var rendimiento in listRendimientos)
            {
                _realm.Write(() => _realm.Add(rendimiento));
            }
        }

        public void SaveUtilidades(List<Utilidad> listUtilidades)
        {
            _realm.Write(() => _realm.RemoveAll<Utilidad>());
            foreach (var utilidad in listUtilidades)
            {
                _realm.Write(() => _realm.Add(utilidad));
            }
        }

        public void UpdateParcel(ParcelDTO parcel)
        {
            _realm.Write(() => _realm.Add(parcel, update: true));
        }
    }
}