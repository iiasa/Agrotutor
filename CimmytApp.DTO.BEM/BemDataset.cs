namespace CimmytApp.DTO.BEM
{
    public abstract class BemDataset
    {
        public string OutCycle => GetCycle();

        public string OutType => GetDataType();

        public string OutValue => GetValue();

        public string OutYear => GetYear();

        public abstract string GetCycle();

        public abstract string GetDataType();

        public abstract string GetValue();

        public abstract string GetYear();
    }
}