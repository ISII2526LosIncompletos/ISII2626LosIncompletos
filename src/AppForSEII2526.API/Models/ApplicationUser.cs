namespace AppForSEII2526.API.Models;

public class ApplicationUser : IdentityUser
{
    //Constructor vacío
    public ApplicationUser()
    {
    }

    public ApplicationUser(string nombre, string apellidos, string direccionEnvio, string correoElectronico, string numTelefono, RolUsuario rol, DateTime fechaRegistro)
    {
        Nombre = nombre;
        Apellidos = apellidos;
        DireccionEnvio = direccionEnvio;
        CorreoElectronico = correoElectronico;
        NumTelefono = numTelefono;
        Rol = rol;
        FechaRegistro = fechaRegistro;
    }

    [Required]
    [Display(Name = "Nombre")]
    [StringLength(20, ErrorMessage = "Nombre no puede superar los 20 caracteres.")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
    public string Nombre { get; set; }

    [Required]
    [Display(Name = "Apellidos")]
    [StringLength(40, ErrorMessage = "Apellidos no puede superar los 40 caracteres.")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
    public string Apellidos { get; set; }

    [Required]
    [Display(Name = "Dirección de envío")]
    [StringLength(50, ErrorMessage = "Dirección no puede superar los 50 caracteres.")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
    public string DireccionEnvio { get; set; }

    [Required]
    [Display(Name = "Correo electrónico")]
    [StringLength(50, ErrorMessage = "Corro electrónico no puede superar los 50 caracteres.")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
    public string? CorreoElectronico { get; set; }

    [Required]
    [Display(Name = "Número de teléfono")]
    [StringLength(9, ErrorMessage = "Número de telefono no puede superar los 9 caracteres.")]
    [RegularExpression(@"^[0-9]*$")]
    public string NumTelefono { get; set; }

    [Display(Name = "Rol de usuario")]
    public RolUsuario Rol { get; set; }

    [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha de registro")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
}

public enum RolUsuario
{
    Administrador,
    Cliente
}
