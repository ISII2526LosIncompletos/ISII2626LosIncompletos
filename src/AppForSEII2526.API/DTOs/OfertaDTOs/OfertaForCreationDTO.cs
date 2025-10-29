namespace AppForSEII2526.API.DTOs.OfertaDTOs
{
    public class OfertaForCreationDTO
    {
        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha final es obligatoria.")]
        public DateTime FechaFinal { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        public string MetodoPago { get; set; }

        public string? DirigidaA { get; set; } 

        [Required]
        [MinLength(1, ErrorMessage = "La oferta debe incluir al menos una herramienta.")]
        public IList<OfertaItemDTO> Items { get; set; }
    }
}
