namespace AppForSEII2526.API.Models
{
    public class Herramienta
    {

        public Herramienta()
        {
        }

        public Herramienta(string material, decimal precio, string nombre, int tiempoReparacion, Fabricante fabricante, int fabricanteId)
        {
            Material = material;
            Precio = precio;
            Nombre = nombre;
            TiempoReparacion = tiempoReparacion;
            Fabricante = fabricante;
        }

        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "El material no puede tener mas de 50 caracteres")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string Material { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Precio de la herramienta")]
        [Precision(5, 2)]
        public decimal Precio { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string Nombre { get; set; }

        [Display(Name = "Tiempo que tarda en repararse la herramienta")]
        [Range(1, int.MaxValue, ErrorMessage = "El m�nimo n�mero de d�as es 1")]
        public int TiempoReparacion { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "El fabricante no puede tener mas de 20 caracteres")]
        public Fabricante Fabricante { get; set; }

        [Required]
        public int FabricanteId { get; set; }

        public IList<AlquilarItem> AlquilarItems { get; set; }
        public IList<CompraItem> CompraItems { get; set; }
        public IList<OfertaItem> OfertaItems { get; set; }
        public IList<ReparacionItem> ItemsReparacion { get; set; }
    }
}