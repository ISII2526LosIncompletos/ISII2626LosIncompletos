namespace AppForSEII2526.API.Models
{
  [PrimaryKey(nameof(IdHerramienta), nameof(IdReparacion))]

public class ReparacionItem
    {
        public ReparacionItem(Herramienta herramienta, int idHerramienta, Reparacion reparacion, 
            int idReparacion, int cantidad, string? descripcion, decimal precio)
        {
            Herramienta = herramienta;
            IdHerramienta = idHerramienta;
            Reparacion = reparacion;
            IdReparacion = idReparacion;
            Cantidad = cantidad;
            Descripcion = descripcion;
            Precio = precio;
        }

        public Herramienta Herramienta { get; set; }
        public int IdHerramienta { get; set; }

        public Reparacion Reparacion { get; set; }
        public int IdReparacion { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser superior a 1.")]
        public int Cantidad { get; set; }

        [StringLength(100, ErrorMessage = "La descripción no puede superar los 100 caracteres.")]
        public string? Descripcion { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Precio reparación")]
        [Precision(10, 2)]
        public decimal Precio { get; set; }
    }
}