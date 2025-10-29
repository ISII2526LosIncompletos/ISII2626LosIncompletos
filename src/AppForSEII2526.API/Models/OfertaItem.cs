namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(IdOferta), nameof(IdHerramienta))]
    public class OfertaItem
    {
        [Required]
        public Herramienta Herramienta { get; set; }

        [Required]
        public int IdHerramienta { get; set; }

        [Required]
        public Oferta Oferta { get; set; }

        [Required]
        public int IdOferta { get; set; }

        [Required]
        [Display(Name = "Porcentaje de oferta")]
        [Range(1, 100, ErrorMessage = "El porcentaje mínimo es 1 y el máximo es 100.")]
        public int Porcentaje { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Precio final oferta")]
        [Precision(5, 2)]
        public decimal PrecioFinal { get; set; }
        public int HerramientaId { get; internal set; }
    }
}

