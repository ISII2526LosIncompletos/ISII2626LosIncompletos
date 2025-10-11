namespace AppForSEII2526.API.Models
{
    public class Reparacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaEntrega { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaRecogida { get; set; }

        [Required]
        [Precision(5, 2)]
        public float precioTotal { get; set; }

        [Required]
        [Display(Name = "Metodos de pago")]
        public TiposMetodosPago metodoPago { get; set; }

        public List<ReparacionItem> ReparacionItem { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public enum TiposMetodosPago
        {
            TarjetaCredito,
            PayPal,
            Cash
        }
    }
}