namespace CimmytApp.BusinessContract
{
    using System.Collections.Generic;
    using CimmytApp.DTO.BEM;
    using CimmytApp.DTO.Parcel;

    public interface ICimmytDbOperations
    {
        void AddParcel(Parcel parcel);

        int DeleteAllData();

        void DeleteParcel(Parcel parcel);

        List<Parcel> GetAllParcels();

        BemData GetBemData();

        Parcel GetParcelById(int parcelId);

        void SaveCostos(List<Costo> listCostos);

        void SaveIngresos(List<Ingreso> listIngresos);

        void SaveParcelPolygon(int parcelId, PolygonDto polygonObj);

        void SaveRendimientos(List<Rendimiento> listRendimientos);

        void SaveUtilidades(List<Utilidad> listUtilidades);

        void UpdateParcel(Parcel parcel);
    }
}