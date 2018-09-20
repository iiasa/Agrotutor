namespace CimmytApp.Core.DTO.Benchmarking
{
    using System.Collections.Generic;

    public class CiatData
    {
        public static CiatData FromResponse(List<CiatResponseData> responseData)
        {
            if (responseData == null) return null;
            CiatData data = new CiatData();
            foreach (CiatResponseData ciatResponseData in responseData)
            {
                var i = 0;
                i++;
            }

            return data;
        }
    }
}
