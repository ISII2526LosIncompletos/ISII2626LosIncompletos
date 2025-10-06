using System;

public class Compra

{
    public int Id { get; set; }

    [StringLength(25, ErrorMessage = "El nombre no puede ser más de 25 caracteres, ni menos de 1.", MinimumLength = 1)]
     public string NombreCliente { get; set; }

    [StringLength(25, ErrorMessage = "El apellido no puede ser más de 25 caracteres, ni menos de 1.", MinimumLength = 1)]
    public string ApellidoCliente { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime fechaCompra { get; set; }

    [Precision(5, 2)]
    public decimal PrecioTotal { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string? teléfono { get; set; }

    [Display(Name = "Metodos de pago")]
    public TiposMetodosPago metodoPago { get; set; }

    [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, escribe la dirección de envío")]
    public string direcciónEnvío { get; set; }

    [DataType(DataType.Email)]
    public string correoElectrónico { get; set; }

    public List<CompraItem> CompraItems { get; set; }

    public ApplicationUser ApplicationUser { get; set; }

    public enum tiposMetodoPago
    {
        TarjetaCredito,
        PayPal,
        Efectivo
    }
}