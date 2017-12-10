namespace CimmytApp.DTO.Parcel
{
    using System.Collections.Generic;
    using Helper.Map;
    using Newtonsoft.Json;

    public class PolygonDto
    {
        private List<GeoPosition> _listPoints;

        public int Id { get; set; }
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