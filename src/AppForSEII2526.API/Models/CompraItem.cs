namespace AppForSEII2526.API.Models
{

    [PrimaryKey(nameof(HerramientaId), nameof(CompraId))]

    public class CompraItem
    {
        public CompraItem()
        {

        }
        public CompraItem(Herramienta herramienta, int herramientaId, Compra compra,
            int compraId, int cantidad, string? descripcion, decimal precio)
        {
            Herramienta = herramienta;
            HerramientaId = herramientaId;
            Compra = compra;
            CompraId = compraId;
            Cantidad = cantidad;
            Descripcion = descripcion;
            Precio = precio;
        }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad mínima de compra es 1")]
        public int Cantidad { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Descripción no puede ser más largo que 150 caracteres.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string Descripcion { get; set; }

        public Compra Compra { get; set; }
        public int CompraId { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Precio para compra")]
        [Precision(7, 2)]
        public decimal Precio { get; set; }

        public Herramienta Herramienta { get; set; }
        public int HerramientaId { get; set; }
    }
}