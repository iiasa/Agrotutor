namespace Helper.Realm
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using CimmytApp.DTO.BEM;
    using Helper.Realm.BusinessContract;
    using Helper.Realm.DTO;
    using Realms;

    public class CimmytDbOperations : ICimmytDbOperations
    {
        private Realm _realm;

        private Realm Realm
        {
            get
            {
                if (_realm != null) return _realm;

                try
                {
                    _realm = DbContext.GetConnection();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message, e);
                }
                return _realm;
            }
        }

        public void AddParcel(ParcelDTO parcel)
        {
            foreach (var agriculturalActivity in parcel.AgriculturalActivities)
            {
                SaveActivity(agriculturalActivity);
            }
            foreach (var technology in parcel.TechnologiesUsed)
            {
                SaveTechnology(technology);
            }
            foreach (var geoPosition in parcel.Delineation)
            {
                SaveGeoPosition(geoPosition);
            }

            if (parcel.Position != null)
            {
                SaveGeoPosition(parcel.Position);
            }
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

        public ParcelDTO GetParcelById(string parcelId)
        {
            return Realm.Find<ParcelDTO>(parcelId);
        }

        public void SaveCostos(List<Costo> listCostos)
        {
            Realm.Write(() => Realm.RemoveAll<Costo>());
            foreach (var costo in listCostos)
            {
                Realm.Write(() => Realm.Add(costo));
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
            Realm.Write(() => Realm.Add(parcel, true));
        }

        private void SaveActivity(AgriculturalActivityDTO agriculturalActivity)
        {
            Realm.Write(() => Realm.Add(agriculturalActivity));
        }

        private void SaveGeoPosition(GeoPositionDTO geoPosition)
        {
            Realm.Write(() => Realm.Add(geoPosition));
        }

        private void SaveTechnology(TechnologyDTO technology)
        {
            Realm.Write(() => Realm.Add(technology));
        }
    }
}