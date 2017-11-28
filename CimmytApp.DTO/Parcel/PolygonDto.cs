namespace CimmytApp.DTO.Parcel
{
    using System.Collections.Generic;
    using Helper.Map;
    using Newtonsoft.Json;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    [Table("Polygon")]
    public class PolygonDto
    {
#pragma warning disable CS0169 // The field 'PolygonDto._listPoints' is never used
        private List<GeoPosition> _listPoints;
#pragma warning restore CS0169 // The field 'PolygonDto._listPoints' is never used

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [TextBlob("ListPointsBlobbed")]
        public List<GeoPosition> ListPoints
        {
            get
            {
                if (ListPointsBlobbed != null)
                {
                    return JsonConvert.DeserializeObject<List<GeoPosition>>(ListPointsBlobbed);
                }

                return null;

                //  return _listPoints;
            }
            set
            {
                if (value != null)
                {
                    ListPointsBlobbed = JsonConvert.SerializeObject(value);
                }

                //  _listPoints = value;
            }
        }

        public string ListPointsBlobbed { get; set; } // serialized Geo Points
    }
}