namespace Helper.DTO.SkywiseWeather.Historical
{
    using System;

    public class Value
    {
        public int ID { get; set; }

        public string validDate { get; set; }

        public DateTime validTime { get; set; }

        public float value { get; set; }
    }
}