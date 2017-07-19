namespace CimmytApp.SQLiteDB
{
    using System.Collections.Generic;
    using CimmytApp.BusinessContract;
    using Helper.BusinessContract;

    public class CimmytDbOperations //: ICimmytDbOperations
    {
        public static List<IDataset> GetAllParcels()
        {
            var parcels = new List<IDataset>();
            // fetch
            return parcels;
        }
    }
}