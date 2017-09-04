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

        public class BenchmarkingDataset
        {
            public String filename { get; set; }
            public float val { get; set; }
            public String year { get; set; }

            public void SetYear()
            {
                year = filename.Substring(filename.Length - 15, filename.Length - 11);
            }
        }

        public void SetYears()
        {
            foreach (var benchmarkingDataset in BenchmarkingDatasets)
            {
                benchmarkingDataset.SetYear();
            }
        }
    }
}