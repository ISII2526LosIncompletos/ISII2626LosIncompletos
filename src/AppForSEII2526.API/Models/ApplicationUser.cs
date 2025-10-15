

namespace AppForSEII2526.API.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [Display(Name = "Nombre")]
    public string nombre { get; set; }

    [Required]
    [Display(Name = "Apellidos")]
    public string apellidos { get; set; }

    [Required]
    [Display(Name = "Dirección de envío")]
    public string direccionEnvio { get; set; }

    [Required]
    [Display(Name = "Correo electrónico")]
    public string corrElectronico { get; set; }

    [Required]
    [Display(Name = "Número de teléfono")]
    public string numTelefono { get; set; }

    [Display(Name = "Rol de usuario")]
    public RolUsuario rol { get; set; }

    [Display(Name = "Fecha de registro")]
    public DateTime fechaRegistro { get; set; } = DateTime.Now;
}

public enum RolUsuario
{
    Administrador,
    Cliente
}