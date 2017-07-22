

namespace Helper.GeoWiki.GenericDatasetStorage
{

	using Newtonsoft.Json;

    class GetDatasetsParams
	{
		[JsonProperty("project_id")]
		public int ProjectId { get; internal set; }

		[JsonProperty("project_version_id")]
		public int ProjectVersionId { get; internal set; }

		[JsonProperty("dataset_group_id")]
		public object DatasetGroupId { get; internal set; }
    }
}
