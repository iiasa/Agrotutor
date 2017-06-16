using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CimmytApp.DTO
{
    [Table("WeatherData")]
    public class WeatherData
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Gdd gdd { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Cdd cdd { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Hdd hdd { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Dp dp { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Hp hp { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Hrh hrh { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Dsr dsr { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Hsr hsr { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]

        public Ht ht { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Dht dht { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Dlt dlt { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Hd hd { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Hws hws { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Hwd hwd { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Desc desc { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Detc detc { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Hesc hesc { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert)]
        public Hetc hetc { get; set; }
        }

        public class Gdd
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
            public float degreeDays { get; set; }
            public string endDate { get; set; }
            public float baseTemperature { get; set; }
            public Series[] series { get; set; }
            public float longitude { get; set; }
            public float latitude { get; set; }
            public Unit unit { get; set; }
        }

        public class Unit
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string validDate { get; set; }
            public float value { get; set; }
        }

        public class Cdd
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
            public float degreeDays { get; set; }
            public string endDate { get; set; }
            public float baseTemperature { get; set; }
            public Series1[] series { get; set; }
            public float longitude { get; set; }
            public float latitude { get; set; }
            public Unit1 unit { get; set; }
        }

        public class Unit1
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series1
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string validDate { get; set; }
            public float value { get; set; }
        }

        public class Hdd
        {
        [
            PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
            public float degreeDays { get; set; }
            public string endDate { get; set; }
            public float baseTemperature { get; set; }
            public Series2[] series { get; set; }
            public float longitude { get; set; }
            public float latitude { get; set; }
            public Unit2 unit { get; set; }
        }

        public class Unit2
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series2
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string validDate { get; set; }
            public float value { get; set; }
        }

        public class Dp
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
            public string endDate { get; set; }
            public Series3[] series { get; set; }
            public float longitude { get; set; }
            public float latitude { get; set; }
            public float precipitation { get; set; }
            public Unit3 unit { get; set; }
        }

        public class Unit3
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series3
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string validDate { get; set; }
            public float value { get; set; }
        }

        public class Hp
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series4[] series { get; set; }
            public float longitude { get; set; }
            public DateTime startTime { get; set; }
            public float latitude { get; set; }
            public DateTime endTime { get; set; }
            public float precipitation { get; set; }
            public Unit4 unit { get; set; }
        }

        public class Unit4
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series4
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime validTime { get; set; }
            public float value { get; set; }
        }

        public class Hrh
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series5[] series { get; set; }
            public float longitude { get; set; }
            public DateTime startTime { get; set; }
            public float latitude { get; set; }
            public DateTime endTime { get; set; }
            public Unit5 unit { get; set; }
        }

        public class Unit5
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series5
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime validTime { get; set; }
            public float value { get; set; }
        }

        public class Dsr
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
            public float solarRadiation { get; set; }
            public string endDate { get; set; }
            public Series6[] series { get; set; }
            public float longitude { get; set; }
            public float latitude { get; set; }
            public Unit6 unit { get; set; }
        }

        public class Unit6
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series6
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string validDate { get; set; }
            public float value { get; set; }
        }

        public class Hsr
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series7[] series { get; set; }
            public float longitude { get; set; }
            public DateTime startTime { get; set; }
            public float latitude { get; set; }
            public DateTime endTime { get; set; }
            public Unit7 unit { get; set; }
        }

        public class Unit7
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series7
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime validTime { get; set; }
            public float value { get; set; }
        }

        public class Ht
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series8[] series { get; set; }
            public float longitude { get; set; }
            public DateTime startTime { get; set; }
            public float latitude { get; set; }
            public DateTime endTime { get; set; }
            public Unit8 unit { get; set; }
        }

        public class Unit8
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series8
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime validTime { get; set; }
            public float value { get; set; }
        }

        public class Dht
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
            public string endDate { get; set; }
            public Series9[] series { get; set; }
            public float longitude { get; set; }
            public float latitude { get; set; }
            public Unit9 unit { get; set; }
        }

        public class Unit9
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series9
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string validDate { get; set; }
            public float value { get; set; }
        }

        public class Dlt
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
            public string endDate { get; set; }
            public Series10[] series { get; set; }
            public float longitude { get; set; }
            public float latitude { get; set; }
            public Unit10 unit { get; set; }
        }

        public class Unit10
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series10
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string validDate { get; set; }
            public float value { get; set; }
        }

        public class Hd
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series11[] series { get; set; }
            public float longitude { get; set; }
            public DateTime startTime { get; set; }
            public float latitude { get; set; }
            public DateTime endTime { get; set; }
            public Unit11 unit { get; set; }
        }

        public class Unit11
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series11
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime validTime { get; set; }
            public float value { get; set; }
        }

        public class Hws
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series12[] series { get; set; }
            public float longitude { get; set; }
            public DateTime startTime { get; set; }
            public float latitude { get; set; }
            public DateTime endTime { get; set; }
            public Unit12 unit { get; set; }
        }

        public class Unit12
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series12
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime validTime { get; set; }
            public float value { get; set; }
        }

        public class Hwd
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series13[] series { get; set; }
            public float longitude { get; set; }
            public DateTime startTime { get; set; }
            public float latitude { get; set; }
            public DateTime endTime { get; set; }
            public Unit13 unit { get; set; }
        }

        public class Unit13
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series13
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime validTime { get; set; }
            public float value { get; set; }
        }

        public class Desc
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
            public string endDate { get; set; }
            public Series14[] series { get; set; }
            public float longitude { get; set; }
            public float latitude { get; set; }
            public Unit14 unit { get; set; }
        }

        public class Unit14
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series14
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string validDate { get; set; }
            public float value { get; set; }
        }

        public class Detc
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string startDate { get; set; }
            public string endDate { get; set; }
            public Series15[] series { get; set; }
            public float longitude { get; set; }
            public float latitude { get; set; }
            public Unit15 unit { get; set; }
        }

        public class Unit15
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series15
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string validDate { get; set; }
            public float value { get; set; }
        }

        public class Hesc
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series16[] series { get; set; }
            public float longitude { get; set; }
            public DateTime startTime { get; set; }
            public float latitude { get; set; }
            public DateTime endTime { get; set; }
            public Unit16 unit { get; set; }
        }

        public class Unit16
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series16
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime validTime { get; set; }
            public float value { get; set; }
        }

        public class Hetc
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public Series17[] series { get; set; }
            public float longitude { get; set; }
            public DateTime startTime { get; set; }
            public float latitude { get; set; }
            public DateTime endTime { get; set; }
            public Unit17 unit { get; set; }
        }

        public class Unit17
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string description { get; set; }
            public string label { get; set; }
        }

        public class Series17
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime validTime { get; set; }
            public float value { get; set; }
        }

    }

