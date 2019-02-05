namespace Agrotutor.Modules.PriceForecasting.Types
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class PriceForecast
    {
        public PriceForecast()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
    }
}
