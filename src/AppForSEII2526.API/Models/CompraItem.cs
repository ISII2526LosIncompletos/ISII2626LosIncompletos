namespace AppForSEII2526.API.Models {

    [PrimaryKey(nameof(IdHerramienta), nameof(IdCompra))]

    public class CompraItem
    {   
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad mínima de compra es 1")]
        public int Cantidad { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Descripción no puede ser más largo que 150 caracteres.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string Descripcion { get; set; }

        public Compra Compra { get; set; }
        public int IdCompra { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Precio para compra")]
        [Precision(7,2)]
        public decimal Precio { get; set; }

        public Herramienta Herramienta { get; set; }
        public int IdHerramienta { get; set; }
    }
}
