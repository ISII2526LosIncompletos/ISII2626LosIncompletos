namespace AppForSEII2526.API.DTOs.CompraDTOs
{
    public class CompraDetalleDTO : CompraCreacionDTO
    {
        public CompraDetalleDTO(int id, string nombreCliente, string apellidoCliente, string direccionEnvio, decimal precioTotal, DateTime fechaCompra, IList<CompraItemDTO> compraItems)
        {
            CompraId= id;
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            DireccionEnvio = direccionEnvio;
            PrecioTotal = precioTotal;
            FechaCompra = fechaCompra;
            CompraItems = compraItems;
        }
        public int CompraId { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string DireccionEnvio { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime FechaCompra { get; set; }
        public IList<CompraItemDTO> CompraItems { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CompraDetalleDTO dTO &&
                   base.Equals(obj) &&
                   CompraId == dTO.CompraId &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidoCliente == dTO.ApellidoCliente &&
                   DireccionEnvio == dTO.DireccionEnvio &&
                   PrecioTotal == dTO.PrecioTotal &&
                   FechaCompra == dTO.FechaCompra &&
                   CompraItems.SequenceEqual(dTO.CompraItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CompraId, NombreCliente, ApellidoCliente, DireccionEnvio, PrecioTotal, FechaCompra, CompraItems);
        }
    }
}
