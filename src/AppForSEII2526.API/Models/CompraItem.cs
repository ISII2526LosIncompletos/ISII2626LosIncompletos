namespace AppForSEII2526.API.Models {
    [PrimaryKey(nameof(IdHerramienta), nameof(IdCompra))]
    public class CompraItem
    {   
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad mínima de compra es 1")]
        public int Cantidad { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Descripción no puede ser mas largo que 150 caracteres.")]
        public string Descripcion { get; set; }

        public Compra Compra { get; set; }
        public int idCompra { get; set; }

        [Required]
        [Precision(7,2)]
        public float precio { get; set; }

        public Herramienta Herramienta { get; set; }
        public int idHerramienta { get; set; }
    }
}
