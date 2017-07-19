namespace CimmytApp.BusinessContract
{
    using System.Collections.Generic;

    using Helper.BusinessContract;

    public interface ICimmytDbOperations
    {
        List<IDataset> GetAllParcels();
    }
}