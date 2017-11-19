namespace Helper.Map
{
    using System.Threading.Tasks;

    public interface IPosition
    {
        bool CheckIfGPSIsEnabled();

        Task<GeoPosition> GetCurrentPosition();

        Task<bool> StopListening();
    }
}