namespace Agrotutor.Modules.GeoWiki.GenericDatasetStorage
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Param;
    using Types;

    public static class GenericDatasetStorage
    {
        public static async Task<List<T>> GetDatasets<T>(int projectId, int projectVersionId, int datasetGroupId)
        {
            var requestParams = new GetDataset
            {
                ProjectId = projectId,
                ProjectVersionId = projectVersionId,
                DatasetGroupId = datasetGroupId
            };

            var datasets = await GeoWikiApi.Post<List<T>>("dev", "getDatasets", requestParams);

            return datasets;
        }

        public static async Task<int> StoreDatasetAsync(object dataset, int userId, int projectId, int projectVersionId,
            int datasetGroupId)
        {
            var uploadData = new UploadData
            {
                Dataset = dataset,
                UserId = userId,
                ProjectId = projectId,
                ProjectVersionId = projectVersionId,
                DatasetGroupId = datasetGroupId
            };

            var insertId = await GeoWikiApi.Post<Integer>("Dev", "storeDataset", uploadData);

            return insertId.getValue();
        }
    }
}
