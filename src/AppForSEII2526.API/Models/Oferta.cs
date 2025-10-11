namespace AppForSEII2526.API.Models

{
    public class Oferta
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaOferta { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El método de pago es obligatorio")]
        [Display(Name = "Método de Pago")]
        public MetodoPago MetodoPago { get; set; }

        [Display(Name = "Oferta dirigida a")]
        public TiposDirigidaOferta? TiposDirigidaOferta { get; set; }

        public IList<OfertaItem> OfertaItems { get; set; }
    }

    public enum MetodoPago
    {
        PayPal,
        Bizum,
        Tarjeta
    }

    public enum TiposDirigidaOferta
    {
        Socios,
        Clientes
    }
}
