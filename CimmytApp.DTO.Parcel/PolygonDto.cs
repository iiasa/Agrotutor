using System.Collections.Generic;
using Helper.Map;
using Newtonsoft.Json;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace CimmytApp.DTO.Parcel
{
    [Table("Polygon")]
    public class PolygonDto
    {
        private List<GeoPosition> _listPoints;

        public PolygonDto()
        {
            //   _listPoints=new List<GeoPosition>();
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [TextBlob("ListPointsBlobbed")]
        public List<GeoPosition> ListPoints
        {
            get
            {
                if (ListPointsBlobbed != null)
                    return JsonConvert.DeserializeObject<List<GeoPosition>>(ListPointsBlobbed);
                else
                {
                    return null;
                }
                //  return _listPoints;
            }
            set
            {
                if (value != null)
                    ListPointsBlobbed = JsonConvert.SerializeObject(value);

                //  _listPoints = value;
            }
        }

        public string ListPointsBlobbed { get; set; } // serialized Geo Points
    }
}