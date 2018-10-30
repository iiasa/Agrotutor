namespace CimmytApp.Core.Map
{
    using System.Threading.Tasks;
    using Helper.Map;

    public interface IPosition
    {
        bool CheckIfGPSIsEnabled();

        Task<GeoPosition> GetCurrentPosition();

        Task<bool> StopListening();
    }
}