namespace Helper.GeoWiki.GenericDatasetStorage
{
    using Newtonsoft.Json;

    internal class UploadData
    {
        [JsonProperty("data")]
        public object Dataset { get; internal set; }

        [JsonProperty("dataset_group_id")]
        public object DatasetGroupId { get; internal set; }

        [JsonProperty("project_id")]
        public int ProjectId { get; internal set; }

        [JsonProperty("project_version_id")]
        public int ProjectVersionId { get; internal set; }

        [JsonProperty("user_id")]
        public int UserId { get; internal set; }
    }
}