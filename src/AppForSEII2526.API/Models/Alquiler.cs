namespace AppForSEII2526.API.Models
{
    public class Alquiler
    {
        [Key]
        public int Id { get; set; }
        public IList<AlquilarItem> AlquilarItems { get; set; }

        [Required]
        public double precioTotal { get; set; }

        [Required]
        public DateTime FechaAlquiler { get; set; }

        public DateTime FechaFin { get; set; }
        public DateTime FechaInicio { get; set; }

        [Required]
        [Display(Name = "Payment Method")]
        public MetodosPago MetodoPagos { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public enum MetodosPago
        {
            TargetaCredito,
            PayPal,
            Efectivo
        }
    }
}