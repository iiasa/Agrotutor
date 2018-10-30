namespace Helper.DTO.SkywiseWeather.Historical
{
    public abstract class HistoricalSeries
    {
        public float latitude { get; set; }

        public float longitude { get; set; }

        public Value[] series { get; set; }

        public Unit unit { get; set; }

        public abstract void Sort();
    }
}