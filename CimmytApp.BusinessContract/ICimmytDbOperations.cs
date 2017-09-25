namespace CimmytApp.BusinessContract
{
    using System.Collections.Generic;

    using DTO.BEM;
    using DTO.Parcel;

    public interface ICimmytDbOperations
    {
        void AddParcel(Parcel parcel);

        void DeleteParcel(Parcel parcel);

        int DeleteAllData();

        List<Parcel> GetAllParcels();

        void SaveParcelPolygon(int parcelId, PolygonDto polygonObj);

        BemData GetBemData();

        Parcel GetParcelById(int parcelId);

        void SaveCostos(List<Costo> listCostos);

        void SaveIngresos(List<Ingreso> listIngresos);

        void SaveRendimientos(List<Rendimiento> listRendimientos);

        void SaveUtilidades(List<Utilidad> listUtilidades);

        void UpdateParcel(Parcel parcel);
    }
}