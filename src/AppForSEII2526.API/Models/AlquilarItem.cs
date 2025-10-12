namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(idHerramienta),
    nameof(idAlquiler))]

    public class AlquilarItem
    {
        [Required]
        public int Cantidad { get; set; }

        public int idAlquiler { get; set; }
        public Alquiler Alquiler { get; set; }
        public Herramienta Herramienta { get; set; }
        public int idHerramienta { get; set; }

        [Required]
        public double precio { get; set; }
    
    }
}