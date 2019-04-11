namespace Agrotutor.Modules.Benchmarking.Types
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class BenchmarkingInformation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string GetAvg => "\tPromedio: " + BenchmarkingDatasets.Average(r => r.val);

        public string GetMax => "\tMáximo: " + BenchmarkingDatasets.Max(r => r.val);

        public string GetMin => "\tMínimo: " + BenchmarkingDatasets.Min(r => r.val);

        public virtual List<BenchmarkingDataset> BenchmarkingDatasets { get; set; }

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
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }

            public string filename { get; set; }

            public float val { get; set; }

            public string year { get; set; }

            public void SetYear() => year = filename.Substring(filename.Length - 15, 4);
        }
    }
}
