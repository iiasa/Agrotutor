namespace CimmytApp.Core.Persistence.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Threading.Tasks;
    using CimmytApp.DTO.BEM;
    using Helper.HTTP;

    public class BemData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public virtual List<Cost> Costo { get; set; }

        public virtual List<Income> Ingreso { get; set; }

        public virtual List<Yield> Rendimiento { get; set; }

        public virtual List<Profit> Utilidad { get; set; }

        public static async Task<BemData> LoadBEMData() 
        {
            var costo = await RequestJson.Get<List<Cost>>(
                "http://104.239.158.49/api.php?type=costo&tkn=E31C5F8478566357BA6875B32DC59");
            var ingreso =
                await RequestJson.Get<List<Income>>(
                    "http://104.239.158.49/api.php?type=rendimiento&tkn=E31C5F8478566357BA6875B32DC59");
            var rendimiento =
                await RequestJson.Get<List<Yield>>(
                    "http://104.239.158.49/api.php?type=ingreso&tkn=E31C5F8478566357BA6875B32DC59");
            var utilidads =
                await RequestJson.Get<List<Profit>>(
                    "http://104.239.158.49/api.php?type=utilidad&tkn=E31C5F8478566357BA6875B32DC59");

            if (costo == null || ingreso == null || rendimiento == null || utilidads == null)
            {
                return null;
            }

            var bemData = new BemData
            {
                Costo = costo,
                Ingreso = ingreso,
                Rendimiento = rendimiento,
                Utilidad = utilidads
            };
            return bemData;
        }
    }
}