namespace CimmytApp.BusinessContract
{
    using System.Collections.Generic;

    using DTO.Parcel;

    public interface ICimmytDbOperations
    {
        void AddParcel(Parcel parcel);

        int DeleteAllData();

        List<Parcel> GetAllParcels();

        Parcel GetParcelById(int parcelId);

        void UpdateParcel(Parcel parcel);
    }
}