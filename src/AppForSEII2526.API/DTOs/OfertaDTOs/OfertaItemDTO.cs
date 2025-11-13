using System.ComponentModel.DataAnnotations;

namespace AppForSEII2526.API.DTOs.OfertaDTOs
{
    public class OfertaItemDTO
    {
        public OfertaItemDTO()
        {
        }

        public OfertaItemDTO(int herramientaId, decimal porcentaje, string herramientaNombre, string herramientaMaterial, 
            string fabricanteNombre, decimal precioOriginal, decimal precioFinal)
        {
            HerramientaId = herramientaId;
            Porcentaje = porcentaje;
            HerramientaNombre = herramientaNombre;
            HerramientaMaterial = herramientaMaterial;
            FabricanteNombre = fabricanteNombre;
            PrecioOriginal = precioOriginal;
            PrecioFinal = precioFinal;
        }

        [Required]
        public int HerramientaId { get; set; }

        [Required]
        [Range(0.01, 100)]
        public decimal Porcentaje { get; set; }

        public string HerramientaNombre { get; set; }
        public string HerramientaMaterial { get; set; }
        public string FabricanteNombre { get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal PrecioFinal { get; set; }

        
        public override bool Equals(object? obj)
        {
            return obj is OfertaItemDTO dTO &&
                   HerramientaId == dTO.HerramientaId &&
                   Porcentaje == dTO.Porcentaje;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HerramientaId, Porcentaje);
        }
    }
}