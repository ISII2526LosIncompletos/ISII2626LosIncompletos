using AppForSEII2526.API.DTOs.HerramientaDTOs;

namespace AppForSEII2526.API.DTOs.CompraDTOs
{
    public class CompraDetalleDTO
    {
        public CompraDetalleDTO(string nombreCliente,
            string apellidoCliente, string direccionEnvio,
             decimal precioTotal,
            IList<HerramientasDTO>compraItems) {
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            DireccionEnvio = direccionEnvio;
            PrecioTotal = precioTotal;
            CompraItems = compraItems;

        }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string DireccionEnvio {  get; set; }
        public decimal PrecioTotal { get; set; }
        public IList<HerramientasDTO> CompraItems {  get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CompraDetalleDTO dTO &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidoCliente == dTO.ApellidoCliente &&
                   DireccionEnvio == dTO.DireccionEnvio &&
                   PrecioTotal == dTO.PrecioTotal &&
                   EqualityComparer<IList<HerramientasDTO>>.Default.Equals(CompraItems, dTO.CompraItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NombreCliente, ApellidoCliente, DireccionEnvio, PrecioTotal, CompraItems);
        }
    }
}
