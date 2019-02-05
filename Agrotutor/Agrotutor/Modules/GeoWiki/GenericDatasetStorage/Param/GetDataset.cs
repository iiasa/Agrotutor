namespace Agrotutor.Modules.GeoWiki.GenericDatasetStorage.Param
{
    using Newtonsoft.Json;

    internal class GetDataset
    {
        [JsonProperty("dataset_group_id")]
        public object DatasetGroupId { get; internal set; }

        [JsonProperty("project_id")]
        public int ProjectId { get; internal set; }

        [JsonProperty("project_version_id")]
        public int ProjectVersionId { get; internal set; }
    }
}
