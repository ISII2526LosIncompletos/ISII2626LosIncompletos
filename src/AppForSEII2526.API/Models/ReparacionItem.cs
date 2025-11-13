namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(HerramientaId), nameof(ReparacionId))]

    public class ReparacionItem
    {

        public ReparacionItem()
        {
        }

        public ReparacionItem(Herramienta herramienta, int herramientaId, Reparacion reparacion,
            int reparacionId, int cantidad, string? descripcion, decimal precio)
        {
            Herramienta = herramienta;
            HerramientaId = herramientaId;
            Reparacion = reparacion;
            ReparacionId = reparacionId;
            Cantidad = cantidad;
            Descripcion = descripcion;
            Precio = precio;
        }

        public Herramienta Herramienta { get; set; }
        public int HerramientaId { get; set; }

        public Reparacion Reparacion { get; set; }
        public int ReparacionId { get; set; }

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