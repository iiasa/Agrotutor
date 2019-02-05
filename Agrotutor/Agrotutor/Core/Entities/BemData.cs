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
    }
}
