namespace AppForSEII2526.API.Models

{
    public class Oferta
    {
        public int Id { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha inicio oferta")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha final oferta")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha oferta")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaOferta { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El método de pago es obligatorio")]
        [Display(Name = "Método de Pago")]
        public tiposMetodosPago MetodoPago { get; set; }

        [Display(Name = "Oferta dirigida a")]
        public TiposDirigidaOferta? TiposDirigidaOferta { get; set; }

        public IList<OfertaItem> OfertaItems { get; set; }
        public object Precio { get; internal set; }
        public object Fabricante { get; internal set; }
        public object Items { get; internal set; }
        public object DirigidaA { get; internal set; }
    }

    public enum TiposDirigidaOferta
    {
        Socios,
        Clientes
    }
}
