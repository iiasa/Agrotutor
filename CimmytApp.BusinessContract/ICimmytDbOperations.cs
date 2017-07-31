namespace CimmytApp.BusinessContract
{
    using System.Collections.Generic;

    using CimmytApp.DTO.Parcel;

    public interface ICimmytDbOperations
    {
        List<Parcel> GetAllParcels();
        void AddParcel(Parcel parcel);
    }
}