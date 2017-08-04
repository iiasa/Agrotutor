namespace CimmytApp.BusinessContract
{
    using System.Collections.Generic;

    using CimmytApp.DTO.Parcel;

    public interface ICimmytDbOperations
    {
        Parcel GetParcelById(int parcelId);
        List<Parcel> GetAllParcels();
        void AddParcel(Parcel parcel);
        int DeleteAllData();
    }
}