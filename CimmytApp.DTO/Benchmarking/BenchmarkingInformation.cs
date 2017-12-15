namespace CimmytApp.DTO.Benchmarking
{
    using System.Collections.Generic;
    using System.Linq;

    public class BenchmarkingInformation
    {
        public string GetAvg
        {
            get
            {
                return "\tPromedio: " + BenchmarkingDatasets.Average(r => r.val);
            }
        }

        public string GetMax
        {
            get
            {
                return "\tMáximo: " + BenchmarkingDatasets.Max(r => r.val);
            }
        }

        public string GetMin
        {
            get
            {
                return "\tMínimo: " + BenchmarkingDatasets.Min(r => r.val);
            }
        }

        public List<BenchmarkingDataset> BenchmarkingDatasets { get; set; }

        public void KeepNewest(int count)
        {
            while (BenchmarkingDatasets.Count > count)
            {
                BenchmarkingDatasets.RemoveAt(0);
            }
        }

        public void SetYears()
        {
            foreach (var benchmarkingDataset in BenchmarkingDatasets)
            {
                benchmarkingDataset.SetYear();
            }
        }

        public class BenchmarkingDataset
        {
            public string filename { get; set; }

            public float val { get; set; }

            public string year { get; set; }

            public void SetYear()
            {
                year = filename.Substring(filename.Length - 15, 4);
            }
        }
    }
}