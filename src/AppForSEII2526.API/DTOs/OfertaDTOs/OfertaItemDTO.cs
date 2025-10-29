namespace AppForSEII2526.API.DTOs.OfertaDTOs
{
    public class OfertaItemDTO
    {
        [Required]
        public int HerramientaId { get; set; }

        [Required(ErrorMessage = "El porcentaje es obligatorio para cada herramienta.")]
        [Range(0.01, 100, ErrorMessage = "El porcentaje debe estar entre 0.01 y 100.")] 
        public double Porcentaje { get; set; }
    }
}
