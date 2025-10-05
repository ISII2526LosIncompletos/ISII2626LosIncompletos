[PrimaryKey(nameof(IdHerramienta), nameof(IdReparacion))]
public class ReparacionItem
{
    public Herramienta Herramienta { get; set; }
    public int IdHerramienta { get; set; }

    public Reparacion Reparacion { get; set; }
    public int IdReparacion { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser superior a 1.")]
    public int Cantidad { get; set; }

    [StringLength(100, ErrorMessage = "La descripción no puede superar los 100 caracteres.")]
    public string? Descripcion { get; set; }

    [Precision(10, 2)]
    public float precio { get; set; }
}