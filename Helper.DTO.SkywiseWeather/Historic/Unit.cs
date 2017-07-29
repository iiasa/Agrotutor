namespace Helper.DTO.SkywiseWeather.Historic
{
	using System;

    public class Unit
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Description { get; set; }
		public string Label { get; set; }
    }
}
