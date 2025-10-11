using Microsoft.AspNetCore.Identity;

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

    [Display(Name = "Correo electronico")]
    public string? correoElectronico { get; set; }

    [Display(Name = "Numero de telefono")]
    public string? numeroTelefono { get; set; }

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