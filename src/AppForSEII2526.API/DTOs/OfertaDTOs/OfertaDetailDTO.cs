

namespace AppForSEII2526.API.DTOs.OfertaDTOs
{
    public class OfertaDetailDTO : OfertaForCreationDTO
    {
        private tiposMetodosPago metodoPago;
        private TiposDirigidaOferta? tiposDirigidaOferta;
        private List<OfertaItemDTO> ofertaItemDTOs;

        public OfertaDetailDTO()
        {
        }

        public OfertaDetailDTO(int id, DateTime fechaInicio, DateTime fechaFinal, DateTime fechaOferta, List<OfertaItemDTO> ofertaItemDTOs)
        {
            Id = id;
            FechaInicio = fechaInicio;
            FechaFinal = fechaFinal;
            FechaOferta = fechaOferta;
            this.ofertaItemDTOs = ofertaItemDTOs;
        }

        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime FechaOferta { get; set; }
        public string MetodoPago { get; set; }
        public string? DirigidaA { get; set; }
        public IList<OfertaDetailDTO> Items { get; set; }

        public OfertaDetailDTO(string herramientaNombre, string herramientaMaterial, string fabricanteNombre,
                                             double precioOriginal, double precioFinal, double porcentaje)
        {
            HerramientaNombre = herramientaNombre;
            HerramientaMaterial = herramientaMaterial;
            FabricanteNombre = fabricanteNombre;
            PrecioOriginal = precioOriginal;
            PrecioFinal = precioFinal;
            Porcentaje = porcentaje;
        }

        [Required]
        public string HerramientaNombre { get; set; }

        public string HerramientaMaterial { get; set; }

        public string FabricanteNombre { get; set; }

        [Required]
        public double PrecioOriginal { get; set; }

        [Required]
        public double PrecioFinal { get; set; }

        [Required]
        public double Porcentaje { get; set; }
    }
}

}
