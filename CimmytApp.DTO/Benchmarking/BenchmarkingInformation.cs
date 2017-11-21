namespace CimmytApp.DTO.Benchmarking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BenchmarkingInformation
    {
        public string GetAvg
        {
            get
            {
                return "\tAverage: " + BenchmarkingDatasets.Average(r => r.val);
            }
        }

        public string GetMax
        {
            get
            {
                return "\tMax: " + BenchmarkingDatasets.Max(r => r.val);
            }
        }

        public string GetMin
        {
            get
            {
                return "\tMin: " + BenchmarkingDatasets.Min(r => r.val);
            }
        }

        public List<BenchmarkingDataset> BenchmarkingDatasets { get; set; }

        public void KeepNewest(int count)
        {
            while (BenchmarkingDatasets.Count() > count)
            {
                BenchmarkingDatasets.RemoveAt(0);
            }
        }

        public void SetYears()
        {
            foreach (BenchmarkingDataset benchmarkingDataset in BenchmarkingDatasets)
            {
                benchmarkingDataset.SetYear();
            }
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
    }
}