using System;
using System.Collections.Generic;

namespace CimmytApp.DTO.BEM
{
    public class BemData
	{
		public List<Costo> Costo { get; set; }
		public List<Ingreso> Ingreso { get; set; }
        public List<Rendimiento> Rendimiento { get; set; }
		public List<Utilidad> Utilidad { get; set; }

        public BemData()
        {
        }
    }
}
