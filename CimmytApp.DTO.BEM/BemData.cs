namespace CimmytApp.DTO.BEM
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Helper.HTTP;

    public class BemData
    {
        public List<Costo> Costo { get; set; }
        public List<Ingreso> Ingreso { get; set; }

        public bool IsEmpty
        {
            get
            {
                var isEmpty = false;
                if ((Costo == null) || (Ingreso == null) || (Rendimiento == null) || (Utilidad == null)) isEmpty = true;
                else if ((Costo.Count == 0) || (Ingreso.Count == 0) || (Rendimiento.Count == 0) ||
                         (Utilidad.Count == 0)) isEmpty = true;
                return isEmpty;
            }
        }

        public List<Rendimiento> Rendimiento { get; set; }
        public List<Utilidad> Utilidad { get; set; }

        public static async Task<BemData> LoadBEMData()
        {
            var taskCosto = RequestJson.Get<List<Costo>>("http://104.239.158.49/api.php?type=costo&tkn=E31C5F8478566357BA6875B32DC59");
            var taskIngreso = RequestJson.Get<List<Ingreso>>("http://104.239.158.49/api.php?type=rendimiento&tkn=E31C5F8478566357BA6875B32DC59");
            var taskRendimiento = RequestJson.Get<List<Rendimiento>>("http://104.239.158.49/api.php?type=ingreso&tkn=E31C5F8478566357BA6875B32DC59");
            var taskUtilidad = RequestJson.Get<List<Utilidad>>("http://104.239.158.49/api.php?type=utilidad&tkn=E31C5F8478566357BA6875B32DC59");

            Task.WaitAll(taskCosto, taskIngreso, taskRendimiento, taskUtilidad);

            var bemData = new BemData()
            {
                Costo = taskCosto.Result,
                Ingreso = taskIngreso.Result,
                Rendimiento = taskRendimiento.Result,
                Utilidad = taskUtilidad.Result
            };
            return bemData;
        }
    }
}