namespace AppForSEII2526.API.DTOs.OfertaDTOs
{
    public class OfertaDetailDTO
    {
        public OfertaDetailDTO() 
        { 
            Items = new List<OfertaItemDTO>();
        }

        public OfertaDetailDTO(int id, DateTime fechaInicio, DateTime fechaFinal, DateTime fechaOferta, 
            string metodoPago, string? dirigidaA, IList<OfertaItemDTO> items)
        {
            Id = id;
            FechaInicio = fechaInicio;
            FechaFinal = fechaFinal;
            FechaOferta = fechaOferta;
            MetodoPago = metodoPago;
            DirigidaA = dirigidaA;
            Items = items;
        }

        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime FechaOferta { get; set; }
        public string MetodoPago { get; set; }
        public string? DirigidaA { get; set; }
        public IList<OfertaItemDTO> Items { get; set; }
    }
}