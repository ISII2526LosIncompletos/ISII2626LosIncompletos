namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(OfertaId), nameof(HerramientaId))]
    public class OfertaItem
    {
        public OfertaItem()
        {
        }
        public OfertaItem(Herramienta herramienta, int herramientaId,
            Oferta oferta, int ofertaId, int porcentaje, decimal precioFinal)
        {
            Herramienta = herramienta;
            HerramientaId = herramientaId;
            Oferta = oferta;
            OfertaId = ofertaId;
            Porcentaje = porcentaje;
            PrecioFinal = precioFinal;
        }

        [Required]
        public Herramienta Herramienta { get; set; }

        [Required]
        public int HerramientaId { get; set; }

        [Required]
        public Oferta Oferta { get; set; }

        [Required]
        public int OfertaId { get; set; }

        [Required]
        [Display(Name = "Porcentaje de oferta")]
        [Range(1, 100, ErrorMessage = "El porcentaje mínimo es 1 y el máximo es 100.")]
        public int Porcentaje { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Precio final oferta")]
        [Precision(5, 2)]
        public decimal PrecioFinal { get; set; }
    }
}