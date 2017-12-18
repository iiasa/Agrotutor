namespace Helper.Realm.BusinessContract
{
    using System.Collections.Generic;
    using CimmytApp.DTO.BEM;
    using Helper.Realm.DTO;

    public interface ICimmytDbOperations
    {
        void AddParcel(ParcelDTO parcel);

        void DeleteParcel(string parcelId);

        List<ParcelDTO> GetAllParcels();

        BemData GetBemData();

        ParcelDTO GetParcelById(string parcelId);

        void SaveCostos(List<Costo> listCostos);

        void SaveIngresos(List<Ingreso> listIngresos);

        void SaveRendimientos(List<Rendimiento> listRendimientos);

        void SaveUtilidades(List<Utilidad> listUtilidades);

        void UpdateParcel(ParcelDTO parcel);
    }
}