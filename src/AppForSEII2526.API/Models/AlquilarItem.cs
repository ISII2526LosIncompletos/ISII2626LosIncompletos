namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(HerramientaId),
    nameof(IdAlquiler))]

    public class AlquilarItem
    {

        public int Cantidad { get; set; }

        public int IdAlquiler { get; set; }
        public Alquiler Alquiler { get; set; }
        public Herramienta herramienta { get; set; }
        public int IdHerramienta { get; set; }

        public double Precio { get; set; }
    
}
}
