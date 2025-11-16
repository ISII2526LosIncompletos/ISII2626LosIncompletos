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

        public override bool Equals(object? obj)
        {
            var dTO = obj as OfertaDetailDTO;
            if (dTO == null) return false;

            bool simplePropsEqual = Id == dTO.Id &&
                                     MetodoPago == dTO.MetodoPago &&
                                     DirigidaA == dTO.DirigidaA;

            if (!simplePropsEqual) return false;

            return Items.SequenceEqual(dTO.Items);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, MetodoPago, DirigidaA, Items);
        }
    }
}