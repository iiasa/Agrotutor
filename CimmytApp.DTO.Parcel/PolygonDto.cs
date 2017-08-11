using System.Collections.Generic;
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
        public List<GeoPosition>ListPoints
        {
            get
            {
                return JsonConvert.DeserializeObject<List<GeoPosition>>(ListPointsBlobbed);
             //  return _listPoints;
            }
            set
            {

                ListPointsBlobbed = JsonConvert.SerializeObject(value);
                
              //  _listPoints = value;
            }
        }
      
        public string ListPointsBlobbed { get; set; } // serialized Geo Points
    }
}
