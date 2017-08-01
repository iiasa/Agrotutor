namespace Helper.Geolocator
{
    using System.Threading.Tasks;
    using CimmytApp.DTO;

    public interface IPosition
    {
        bool CheckIfGpsIsEnabled();

        Task<GeoPosition> GetCurrentPosition();
    }
}