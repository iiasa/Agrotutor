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
            IFlurlRequest request = Constants
                .MatrizBaseUrl
                .SetQueryParams(
                    new
                    {
                        lat = position.Latitude,
                        lon = position.Longitude,
                        type = "matriz",
                        tkn = Constants.BemToken,
                        cultivo = crop
                    })
                .WithBasicAuth(Constants.BemUsername, Constants.BemPassword);

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
