namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(IdHerramienta),
    nameof(IdAlquiler))]

    public class AlquilarItem
    {
        [Required]
        public int Cantidad { get; set; }

        public int IdAlquiler { get; set; }
        public Alquiler Alquiler { get; set; }
        public Herramienta herramienta { get; set; }
        public int IdHerramienta { get; set; }

        [Required]
        public double Precio { get; set; }
    
}
}
