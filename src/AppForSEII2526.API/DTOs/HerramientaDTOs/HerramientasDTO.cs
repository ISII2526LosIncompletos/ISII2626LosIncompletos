
namespace AppForSEII2526.API.DTOs.CompraDTOs
{
    public class HerramientasDTO
    {
        public HerramientasDTO(string nombre, string material, string fabricante, float precio,int cantidad, string descripcion = "")
        {
            Nombre = nombre;
            Material = material;
            Fabricante = fabricante;
            Precio = precio;
            Cantidad = cantidad;
            Descripcion = descripcion;
            

        }
        public string Nombre { get; set; }
        public string Material { get; set; }
        public string Fabricante { get; set; }
        public float Precio { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
       

        public override bool Equals(object? obj)
        {
            return obj is HerramientasDTO dTO &&
                   Nombre == dTO.Nombre &&
                   Material == dTO.Material &&
                   Fabricante == dTO.Fabricante &&
                   Precio == dTO.Precio &&
                   Descripcion == dTO.Descripcion &&
                   Cantidad == dTO.Cantidad;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nombre, Material, Fabricante, Precio, Descripcion, Cantidad);
        }
    }
}
