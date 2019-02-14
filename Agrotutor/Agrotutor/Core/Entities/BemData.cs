using System.Linq;

namespace Agrotutor.Core.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BemData
    {
        public virtual List<Cost> Cost { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public virtual List<Income> Income { get; set; }

        public virtual List<Yield> Yield { get; set; }

        public virtual List<Profit> Profit { get; set; }

        public string Date { get; set; }

        public float? AverageCost
        {
            get
            {
                float? average = null;
                try
                {
                    average = Cost.Average(x => float.Parse(x.ProductionCost));
                }
                catch
                {
                    average = null;
                }

                return average;
            }
        }

        public float? AverageIncome
        {
            get
            {
                float? average = null;
                try
                {
                    average = Income.Average(x => float.Parse(x.IncomePerHa));
                }
                catch
                {
                    average = null;
                }

                return average;
            }
        }

        public float? AverageYield
        {
            get
            {
                float? average = null;
                try
                {
                    average = Yield.Average(x => float.Parse(x.Performance));
                }
                catch
                {
                    average = null;
                }

                return average;
            }
        }

        public float? AverageProfit
        {
            get
            {
                float? average = null;
                try
                {
                    average = Profit.Average(x => float.Parse(x.Rentability));
                }
                catch
                {
                    average = null;
                }

                return average;
            }
        }
    }
}
