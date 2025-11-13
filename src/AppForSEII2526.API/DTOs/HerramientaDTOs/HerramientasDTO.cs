
namespace AppForSEII2526.API.DTOs.HerramientaDTOs
{
    public class HerramientasDTO
    {
        public HerramientasDTO(int id, string nombre, string material, Fabricante fabricante, decimal precio, int tiempoReparacion)
        {
            HerramientaID = id;
            Nombre = nombre;
            Material = material;
            Fabricante = fabricante;
            Precio = precio;
            TiempoReparacion = tiempoReparacion;
        }

        //Constructor vacio
        public HerramientasDTO()
        {
        }

        public int HerramientaID { get; set; }

        [StringLength(25, ErrorMessage = "El nombre no puede tener más de 25 caracteres.")]
        public string Nombre { get; set; }
        public string Material { get; set; }
        public Fabricante Fabricante { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.05, float.MaxValue, ErrorMessage = "El precio minimo es 0.05")]
        [Precision(10, 2)]
        public decimal Precio { get; set; }
        public int TiempoReparacion { get; set; }
        public string? Descripcion { get; set; }
        public int Cantidad { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is HerramientasDTO dTO &&
                   HerramientaID == dTO.HerramientaID &&
                   Nombre == dTO.Nombre &&
                   Material == dTO.Material &&
                   Fabricante == dTO.Fabricante &&
                   Precio == dTO.Precio &&
                   TiempoReparacion == dTO.TiempoReparacion &&
                   Cantidad == dTO.Cantidad &&
                   Descripcion == dTO.Descripcion;

        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HerramientaID, Nombre, Material, Fabricante, Precio, TiempoReparacion, Descripcion, Cantidad);
        }
    }

}