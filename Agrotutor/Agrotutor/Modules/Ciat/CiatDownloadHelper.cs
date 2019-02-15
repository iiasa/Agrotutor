using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agrotutor.Core.Entities;
using Agrotutor.Modules.Ciat.Types;
using Flurl;
using Flurl.Http;

namespace Agrotutor.Modules.Ciat
{
    public static class CiatDownloadHelper
    {

        public static async Task<CiatData> LoadData(Position position, string crop)
        {
            IFlurlRequest request = "http://104.239.158.49".AppendPathSegment("matrizv2.php")
                .SetQueryParams(
                    new
                    {
                        lat = position.Latitude,
                        lon = position.Longitude,
                        type = "matriz",
                        tkn = "E31C5F8478566357BA6875B32DC59",
                        cultivo = crop
                    })
                .WithBasicAuth("cimmy2018", "tBTAibgFtHxaNE8ld7hpKKsx3n1ORIO");

            List<CiatResponseData> responseData;

            try
            {
                responseData = await request.GetJsonAsync<List<CiatResponseData>>();
            }
            catch (Exception e)
            {
                return null;
            }

            return CiatData.FromResponse(responseData, request.Url);
        }
    }
}
