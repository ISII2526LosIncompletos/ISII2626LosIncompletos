namespace AppForSEII2526.API.DTOs.HerramientaDTOs
{
    public class HerramientasDTO
    {
        public HerramientasDTO(int herramientaID, string nombre, string material, string fabricante, decimal precio, DateTime tiempoReparacion)
        {
            HerramientaID = herramientaID;
            Nombre = nombre;
            Material = material;
            Fabricante = fabricante;
            Precio = precio;
            TiempoReparacion = tiempoReparacion;
        }

        public int HerramientaID { get; set; }
        public string Nombre { get; set; }
        public string Material { get; set; }
        public string Fabricante { get; set; }
        public decimal Precio { get; set; }
        public DateTime TiempoReparacion { get; set; }
        public string? Descripcion { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is HerramientasDTO dTO &&
                   HerramientaID == dTO.HerramientaID &&
                   Nombre == dTO.Nombre &&
                   Material == dTO.Material &&
                   Fabricante == dTO.Fabricante &&
                   Precio == dTO.Precio &&
                   TiempoReparacion == dTO.TiempoReparacion;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HerramientaID, Nombre, Material, Fabricante, Precio, TiempoReparacion);
        }
    }
}
