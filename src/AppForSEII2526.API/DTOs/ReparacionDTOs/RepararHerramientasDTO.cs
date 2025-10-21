
namespace AppForSEII2526.API.DTOs.ReparacionDTOs
{
    public class RepararHerramientasDTO
    {
        public RepararHerramientasDTO(int herramientaID, string nombre, string material, string fabricante, float precioReparacion, DateTime tiempoReparacion, string descripcion="")
        {
            HerramientaID = herramientaID;
            Nombre = nombre;
            Material = material;
            Fabricante = fabricante;
            PrecioReparacion = precioReparacion;
            TiempoReparacion = tiempoReparacion;
            Descripcion = descripcion;
        }

        public int HerramientaID { get; set; }
        public string Nombre { get; set; }
        public string Material { get; set; }
        public string Fabricante { get; set; }
        public float PrecioReparacion { get; set; }
        public DateTime TiempoReparacion { get; set; }
        public string? Descripcion { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is RepararHerramientasDTO dTO &&
                   HerramientaID == dTO.HerramientaID &&
                   Nombre == dTO.Nombre &&
                   Material == dTO.Material &&
                   Fabricante == dTO.Fabricante &&
                   PrecioReparacion == dTO.PrecioReparacion &&
                   TiempoReparacion == dTO.TiempoReparacion &&
                   Descripcion == dTO.Descripcion;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HerramientaID, Nombre, Material, Fabricante, PrecioReparacion, TiempoReparacion, Descripcion);
        }
    }
}
