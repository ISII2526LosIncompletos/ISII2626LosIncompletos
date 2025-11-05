
namespace AppForSEII2526.API.DTOs.CompraDTOs
{
    public class CompraItemDTO
    {
        public CompraItemDTO(int herramientaID, string nombre, string material,decimal precioCompra, int cantidad, string? descripcion="")
        {
            HerramientaID = herramientaID;
            Nombre = nombre;
            Material = material;
            PrecioCompra = precioCompra;
            Cantidad = cantidad;
            Descripcion = descripcion;
        }
        public int HerramientaID { get; set; }
        public string Nombre { get; set; }
        public string Material { get; set; }
        public decimal PrecioCompra { get; set; }
        public int Cantidad { get; set; }
        public string? Descripcion { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CompraItemDTO dTO &&
                   HerramientaID == dTO.HerramientaID &&
                   Nombre == dTO.Nombre &&
                   Material == dTO.Material &&
                   PrecioCompra == dTO.PrecioCompra &&
                   Cantidad == dTO.Cantidad &&
                   Descripcion == dTO.Descripcion;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HerramientaID, Nombre, Material, PrecioCompra, Cantidad, Descripcion);
        }
    }
}
