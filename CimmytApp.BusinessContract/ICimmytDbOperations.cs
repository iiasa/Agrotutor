namespace CimmytApp.BusinessContract
{
    using System.Collections.Generic;

    using DTO.BEM;
    using DTO.Parcel;

    public interface ICimmytDbOperations
    {
        void AddParcel(Parcel parcel);

        int DeleteAllData();

        List<Parcel> GetAllParcels();

        BemData GetBemData();

        Parcel GetParcelById(int parcelId);

        void SaveCostos(List<Costo> listCostos);

        void SaveIngresos(List<Ingreso> listIngresos);

        void SaveRendimientos(List<Rendimiento> listRendimientos);

        void SaveUtilidades(List<Utilidad> listUtilidades);

        void UpdateParcel(Parcel parcel);
    }
}