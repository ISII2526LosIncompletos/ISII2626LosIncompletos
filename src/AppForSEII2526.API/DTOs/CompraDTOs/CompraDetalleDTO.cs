using AppForSEII2526.API.DTOs.HerramientaDTOs;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.CompraDTOs
{
    public class CompraDetalleDTO : CompraCreacionDTO
    {
        public CompraDetalleDTO(string nombreCliente, string apellidoCliente, string direccionEnvio, decimal precioTotal, DateTime fechaCompra, IList<CompraItem> compraItems)
        {
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            DireccionEnvio = direccionEnvio;
            PrecioTotal = precioTotal;
            FechaCompra = fechaCompra;
            CompraItems = compraItems;
        }
        public string NombreCliente {  get; set; }
        public string ApellidoCliente {  get; set; }
        public string DireccionEnvio {  get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime FechaCompra {  get; set; }
        public IList<CompraItem> CompraItems {  get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CompraDetalleDTO dTO &&
                   base.Equals(obj) &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidoCliente == dTO.ApellidoCliente &&
                   DireccionEnvio == dTO.DireccionEnvio &&
                   PrecioTotal == dTO.PrecioTotal &&
                   FechaCompra == dTO.FechaCompra &&
                   CompraItems.SequenceEqual(dTO.CompraItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NombreCliente, ApellidoCliente, DireccionEnvio, PrecioTotal, FechaCompra, CompraItems);
        }
    }
}
