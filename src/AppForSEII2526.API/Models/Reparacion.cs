public class Reparacion
{
    [Key]
    public int Id { get; set; }

    [StringLength(20, ErrorMessage = "Nombre no puede tener más de 20 caracteres ni menos de 1", MinimumLength = 1)]
    public string nombreCliente { get; set; }

    [StringLength(20, ErrorMessage = "Apellido no puede tener más de 20 caracteres ni menos de 1", MinimumLength = 1)]
    public string apellidoCliente { get; set; }

    [DataType(DataType.Currency)]
    [DisplayFormat(DtataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime fechaEntrega { get; set; }

    [DataType(DataType.Currency)]
    [DisplayFormat(DtataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime fechaRecogida { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string? numTelefono { get; set; } // Puede ser nulo, es decir, opcional

    [Precision(5, 2)]
    public float precioTotal { get; set; }

    [Display(Name = "Metodos de pago")]
    public TiposMetodosPago metodoPago { get; set; }

    public List<ReparacionItem> ReparacionItem { get; set; }

    public ApplicationUser ApplicationUser { get; set; }

    public enum TiposMetodosPago
    {
        TarjetaCredito,
        PayPal,
        Cash
    }
}