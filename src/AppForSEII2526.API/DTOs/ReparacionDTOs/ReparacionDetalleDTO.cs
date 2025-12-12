using AppForSEII2526.API.DTOs.HerramientaDTOs;

namespace AppForSEII2526.API.DTOs.ReparacionDTOs
{
    public class ReparacionDetalleDTO : ReparacionCreacionDTO
    {

        public ReparacionDetalleDTO(int id, string nombreCliente, string apellidoCliente, DateTime fechaEntrega,
            DateTime fechaRecogida, string? numTelefono, IList<ReparacionItemDTO> itemsReparacion)
        {
            ReparacionId = id;
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            NumTelefono = numTelefono;
            ItemsReparacion = itemsReparacion;
        }

        public int ReparacionId { get; set; }

        public string NombreCliente { get; set; }

        public string ApellidoCliente { get; set; }

        public DateTime FechaEntrega { get; set; }

        public DateTime FechaRecogida { get; set; }

        public string? NumTelefono { get; set; }

        public IList<ReparacionItemDTO> ItemsReparacion { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ReparacionDetalleDTO dTO &&
                   ReparacionId == dTO.ReparacionId &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidoCliente == dTO.ApellidoCliente &&
                   FechaEntrega == dTO.FechaEntrega &&
                   FechaRecogida == dTO.FechaRecogida &&
                   NumTelefono == dTO.NumTelefono &&
                   ItemsReparacion.SequenceEqual(dTO.ItemsReparacion);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ReparacionId, NombreCliente, ApellidoCliente, FechaEntrega, FechaRecogida, NumTelefono, ItemsReparacion);
        }
    }
}