using System;
namespace CimmytApp.DTO.BEM
{
    public abstract class BemDataset
	{
		public string OutYear { get { return GetYear(); } }
		public string OutCycle { get { return GetCycle(); } }
		public string OutType { get { return GetDataType(); } }
		public string OutValue { get { return GetValue(); } }

		public abstract string GetCycle();
		public abstract string GetYear();
		public abstract string GetDataType();
		public abstract string GetValue();
    }
}
