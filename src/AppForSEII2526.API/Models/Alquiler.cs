namespace AppForSEII2526.API.Models
{
    public class Alquiler
    {
        public int Id { get; set; }

        public IList<AlquilarItem> AlquilarItems { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Price For Renting")]
        [Precision(5, 2)]
        public decimal PrecioTotal { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha alquiler")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaAlquiler { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha fin")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha inicio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Display(Name = "Tipos metodos de pago")]
        public tiposMetodosPago MetodoPagos { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
    public enum tiposMetodosPago
    {
        TarjetaCredito,
        PayPal,
        Efectivo
    }

}