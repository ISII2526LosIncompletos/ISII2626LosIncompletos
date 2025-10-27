namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(IdHerramienta),
    nameof(IdAlquiler))]

    public class AlquilarItem
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad mínima 1")]
        public int Cantidad { get; set; }

        public Alquiler Alquiler { get; set; }
        public int IdAlquiler { get; set; }

        public Herramienta Herramienta { get; set; }
        public int IdHerramienta { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Precio para alquiler")]
        [Precision(5, 2)]
        public decimal Precio { get; set; }
    
    }
}