
using AppForSEII2526.API.DTOs.HerramientaDTOs;

namespace AppForSEII2526.API.DTOs.ReparacionDTOs
{
    public class ReparacionDetalleDTO
    {
        public ReparacionDetalleDTO(string nombreCliente, 
            string apellidoCliente, DateTime fechaEntrega, 
            DateTime fechaRecogida, float precioTotal, 
            IList<HerramientasDTO> reparacionItems)
        {
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            PrecioTotal = precioTotal;
            ReparacionItems = reparacionItems;
        }

        public string NombreCliente { get; set; }

        public string ApellidoCliente { get; set; }

        public DateTime FechaEntrega { get; set; }

        public DateTime FechaRecogida { get; set; }
        
        public float PrecioTotal { get; set; }

        public IList<HerramientasDTO> ReparacionItems { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ReparacionDetalleDTO dTO &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidoCliente == dTO.ApellidoCliente &&
                   FechaEntrega == dTO.FechaEntrega &&
                   FechaRecogida == dTO.FechaRecogida &&
                   PrecioTotal == dTO.PrecioTotal &&
                   EqualityComparer<IList<HerramientasDTO>>.Default.Equals(ReparacionItems, dTO.ReparacionItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NombreCliente, ApellidoCliente, FechaEntrega, FechaRecogida, PrecioTotal, ReparacionItems);
        }
    }
}
