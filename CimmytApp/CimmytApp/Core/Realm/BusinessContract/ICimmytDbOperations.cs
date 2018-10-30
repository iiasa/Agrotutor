namespace Helper.Realm.BusinessContract
{
    using System.Collections.Generic;
    using CimmytApp.Core.Realm.BEM;
    using CimmytApp.DTO.BEM;
    using Helper.Realm.DTO;

    public interface ICimmytDbOperations
    {
        void SaveParcel(ParcelDTO parcel, bool update = false);

        void DeleteParcel(string parcelId);

        List<ParcelDTO> GetAllParcels();

        BemData GetBemData();

        ParcelDTO GetParcelById(string parcelId);

        void SaveCostos(List<Costo> listCostos);

        void SaveIngresos(List<Ingreso> listIngresos);

        void SaveRendimientos(List<Rendimiento> listRendimientos);

        void SaveUtilidades(List<Utilidad> listUtilidades);/*

        List<AgriculturalActivityDTO> GetAgriculturalActivitiesForParcel(string parcelId);

        GeoPositionDTO GetParcelPosition(string parcelId);

        List<GeoPositionDTO> GetParcelDelineation(string parcelId);

        List<TechnologyDTO> GetParcelTechnology(string parcelId);*/
    }
}