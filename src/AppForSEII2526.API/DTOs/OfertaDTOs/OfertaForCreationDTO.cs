using System.ComponentModel.DataAnnotations;

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
        public IList<OfertaItemInput> Items { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is OfertaForCreationDTO dTO &&
                   FechaInicio == dTO.FechaInicio &&
                   FechaFinal == dTO.FechaFinal &&
                   MetodoPago == dTO.MetodoPago &&
                   DirigidaA == dTO.DirigidaA &&
                   Items.SequenceEqual(dTO.Items); 
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FechaInicio, FechaFinal, MetodoPago, DirigidaA, Items);
        }
    }

    public class OfertaItemInput
    {
        [Required]
        public int HerramientaId { get; set; }

        [Required]
        [Range(0.01, 100, ErrorMessage = "El porcentaje debe estar entre 0.01 y 100")]
        public decimal Porcentaje { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is OfertaItemInput item &&
                   HerramientaId == item.HerramientaId &&
                   Porcentaje == item.Porcentaje;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HerramientaId, Porcentaje);
        }
    }
}