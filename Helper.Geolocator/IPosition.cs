
using System;
using System.Threading.Tasks;
using CimmytApp.DTO;

namespace Helper.Geolocator
{
	public interface IPosition
	{
		bool CheckIfGPSIsEnabled();
		Task<GeoPosition> GetCurrentPosition();
	}
}
