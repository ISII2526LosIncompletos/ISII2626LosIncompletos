namespace AppForSEII2526.API.DTOs.HerramientaDTOs
{
    public class HerramientasDTO
    {
        public HerramientasDTO(int herramientaID, string nombre, string material, string fabricante, decimal precio, string descripcion="")
        {
            HerramientaID = herramientaID;
            Nombre = nombre;
            Material = material;
            Fabricante = fabricante;
            Precio = precio;
            Descripcion = descripcion;
            Cantidad = Cantidad;
        }

        public int HerramientaID { get; set; }
        public string Nombre { get; set; }
        public string Material { get; set; }
        public string Fabricante { get; set; }
        public decimal Precio { get; set; }
        public string? Descripcion { get; set; }
        public int Cantidad {  get; set; }

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
