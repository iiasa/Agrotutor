namespace Helper.GeoWiki.API.GenericDatasetStorage
{
    using Newtonsoft.Json;

    internal class GetDatasetsParams
    {
        [JsonProperty("dataset_group_id")]
        public object DatasetGroupId { get; internal set; }

        [JsonProperty("project_id")]
        public int ProjectId { get; internal set; }

        [JsonProperty("project_version_id")]
        public int ProjectVersionId { get; internal set; }
    }
}