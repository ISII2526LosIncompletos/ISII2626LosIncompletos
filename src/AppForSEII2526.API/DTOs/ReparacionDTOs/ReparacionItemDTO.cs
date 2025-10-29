namespace AppForSEII2526.API.DTOs.ReparacionDTOs
{
    public class ReparacionItemDTO
    {
        public ReparacionItemDTO(int herramientaID, string nombre, decimal precioReparacion, int cantidad, string? descripcion = "")
        {
            HerramientaID = herramientaID;
            Nombre = nombre;
            PrecioReparacion = precioReparacion;
            Cantidad = cantidad;
            Descripcion = descripcion;
        }

        public int HerramientaID { get; set; }

        public string Nombre { get; set; }

        public decimal PrecioReparacion { get; set; }

        public int Cantidad { get; set; }

        public string? Descripcion { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ReparacionItemDTO dTO &&
                   HerramientaID == dTO.HerramientaID &&
                   Nombre == dTO.Nombre &&
                   PrecioReparacion == dTO.PrecioReparacion &&
                   Cantidad == dTO.Cantidad &&
                   Descripcion == dTO.Descripcion;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HerramientaID, Nombre, PrecioReparacion, Cantidad, Descripcion);
        }
    }
}
