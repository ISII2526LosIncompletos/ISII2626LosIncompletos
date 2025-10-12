namespace AppForSEII2526.API.Models
{
    public class Herramienta
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "El material no puede tener mas de 30 caracteres")]
        public string Material { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.5, float.MaxValue, ErrorMessage = "El precioMinimo es 1 ")]
        [Display(Name = "Precio")]
        [Precision(10, 2)]
        public decimal Precio { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [Range(0, int.MaxValue, ErrorMessage = "Error nombre")]
        public int Nombre { get; set; }


        [Display(Name = "TiempoReparacion")]
        [Range(0, int.MaxValue, ErrorMessage = "Error tiempo reparacion ")]
        public int tiempoReparacion { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El fabricante no puede tener mas de 30 caracteres")]
        public string Fabricante { get; set; }

        public IList<AlquilarItem> AlquilarItems { get; set; }
        public IList<CompraItem> CompraItems { get; set; }
        public IList<OfertaItem> OfertaItems { get; set; }
        public IList<ItemReparacion> ItemsReparacion { get; set; }
    }
}
