using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CimmytApp.DTO.Benchmarking
{
    public class BenchmarkingInformation
    {
        public List<BenchmarkingDataset> BenchmarkingDatasets { get; set; }

        public string GetMin
        {
            get { return "\tMin: " + BenchmarkingDatasets.Min(r => r.val); }
        }

        public string GetMax
        {
            get { return "\tMax: " + BenchmarkingDatasets.Max(r => r.val); }
        }

        public string GetAvg
        {
            get { return "\tAverage: " + BenchmarkingDatasets.Average(r => r.val); }
        }

        public class BenchmarkingDataset
        {
            public String filename { get; set; }
            public float val { get; set; }
            public String year { get; set; }

            public void SetYear()
            {
                year = filename.Substring(filename.Length - 15, 4);
            }
        }

        public void SetYears()
        {
            foreach (var benchmarkingDataset in BenchmarkingDatasets)
            {
                benchmarkingDataset.SetYear();
            }
        }

        public void KeepNewest(int count)
        {
            BenchmarkingDatasets = (System.Collections.Generic.List<CimmytApp.DTO.Benchmarking.BenchmarkingInformation.BenchmarkingDataset>)BenchmarkingDatasets.Skip(Math.Max(0, BenchmarkingDatasets.Count() - count));
        }
    }
}