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
            if (parcel.ParcelId == null)
            {
                var parcels = GetAllParcels();
                if (parcels.Count > 0)
                    parcel.ParcelId = parcels.Max(p => p.ParcelId).Value + 1;
                else
                    parcel.ParcelId = 0;
            }
            foreach (var agriculturalActivity in parcel.AgriculturalActivities)
                SaveActivity(agriculturalActivity);
            foreach (var technology in parcel.TechnologiesUsed)
                SaveTechnology(technology);
            foreach (var geoPosition in parcel.Delineation)
                SaveGeoPosition(geoPosition);
            if (parcel.Position != null)
                SaveGeoPosition(parcel.Position);
            Realm.Write(() => Realm.Add(parcel));
        }

        private void SaveGeoPosition(GeoPositionDTO geoPosition)
        {
            if (geoPosition.Id == null)
            {
                var positions = Realm.All<GeoPositionDTO>().ToList();
                if (positions.Count > 0)
                    geoPosition.Id = positions.Max(p => p.Id).Value + 1;
                else
                    geoPosition.Id = 0;
            }
            Realm.Write(() => Realm.Add(geoPosition));
        }

        private void SaveTechnology(TechnologyDTO technology)
        {
            if (technology.Id == null)
            {
                var technologies = Realm.All<TechnologyDTO>().ToList();
                if (technologies.Count > 0)
                    technology.Id = technologies.Max(p => p.Id).Value + 1;
                else
                    technology.Id = 0;
            }
            Realm.Write(() => Realm.Add(technology));
        }

        private void SaveActivity(AgriculturalActivityDTO agriculturalActivity)
        {
            Realm.Write(() => Realm.Add(agriculturalActivity));
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
            Realm.Write(() => Realm.Add(parcel, update: true));
        }
    }
}