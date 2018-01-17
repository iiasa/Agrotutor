﻿namespace Helper.Realm
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
        private static int realmVersion = 1;
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

        public void DeleteParcel(string parcelId)
        {
            Realm.Write(() => Realm.Remove(Realm.Find<ParcelDTO>(parcelId)));
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
            var parcelDTO = Realm.Find<ParcelDTO>(parcelId);
            return parcelDTO;
        }/*

        public List<AgriculturalActivityDTO> GetAgriculturalActivitiesForParcel(string parcelId)
        {
            var activities = Realm.All<AgriculturalActivityDTO>().Where(p => p.ParcelId == parcelId);
            return activities.ToList();
        }

        public GeoPositionDTO GetParcelPosition(string parcelId)
        {
            var position = Realm.All<GeoPositionDTO>().Where(p => p.ParcelId == parcelId).First(p => p.IsPartOfdelineation == false);
            return position;
        }

        public List<GeoPositionDTO> GetParcelDelineation(string parcelId)
        {
            var delineation = Realm.All<GeoPositionDTO>().Where(p => p.ParcelId == parcelId).Where(p => p.IsPartOfdelineation);
            return delineation.ToList();
        }

        public List<TechnologyDTO> GetParcelTechnology(string parcelId)
        {
            var tech = Realm.All<TechnologyDTO>().Where(p => p.ParcelId == parcelId);
            return tech.ToList();
        }*/

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

        public void SaveParcel(ParcelDTO parcel, bool update = false)
        { // TODO! delete removed technology
            foreach (var agriculturalActivity in parcel.AgriculturalActivitiesList)
            {
                Realm.Write(() => parcel.AgriculturalActivities.Add(agriculturalActivity));
            }
            foreach (var technology in parcel.TechnologiesUsedList)
            {
                Realm.Write(() => parcel.TechnologiesUsed.Add(technology));
            }
            foreach (var geoPosition in parcel.DelineationList)
            {
                Realm.Write(() => parcel.Delineation.Add(geoPosition));
            }

            if (parcel.Position != null)
            {
                SaveGeoPosition(parcel.Position, update);
            }
            Realm.Write(() => Realm.Add(parcel, update));
        }

        private void SaveActivity(AgriculturalActivityDTO agriculturalActivity, bool update = false)
        {
            Realm.Write(() => Realm.Add(agriculturalActivity, update));
        }

        private void SaveGeoPosition(GeoPositionDTO geoPosition, bool update = false)
        {
            Realm.Write(() => Realm.Add(geoPosition, update));
        }

        private void SaveTechnology(TechnologyDTO technology, bool update = false)
        {
            Realm.Write(() => Realm.Add(technology, update));
        }
    }
}