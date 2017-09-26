using System;
namespace CimmytApp.Benchmarking
{
    public class PhuAccumulator
    {
        double minTemperature;

        public double MinTemperature
        {
            get
            {
                return minTemperature;
            }

            set
            {
                minTemperature = value;
            }
        }
        double maxTemperature;

        public double MaxTemperature
        {
            get
            {
                return maxTemperature;
            }

            set
            {
                maxTemperature = value;
            }
        }

        public PhuAccumulator()
        {
        }
    }
}
