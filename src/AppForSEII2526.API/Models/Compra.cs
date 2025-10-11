namespace AppForSEII2526.API.Models
{

    public class Compra

    {
        [Key]
        public int Id { get; set; }


        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaCompra { get; set; }

        [Required]
        [Precision(5, 2)]
        public decimal PrecioTotal { get; set; }


        [Required]
        [Display(Name = "Metodos de pago")]
        public tiposMetodoPago metodoPago { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        public string correoElectrónico { get; set; }

        public List<CompraItem> CompraItems { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public enum tiposMetodoPago
        {
            TarjetaCredito,
            PayPal,
            Efectivo
        }
    }
}