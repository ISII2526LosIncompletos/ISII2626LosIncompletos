

namespace AppForSEII2526.API.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; }

    [Required]
    [Display(Name = "Apellidos")]
    public string Apellidos { get; set; }

    [Required]
    [Display(Name = "Dirección de envío")]
    public string DireccionEnvio { get; set; }

    [Display(Name = "Rol de usuario")]
    public RolUsuario Rol { get; set; }

    [Display(Name = "Fecha de registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
}

public enum RolUsuario
{
    Administrador,
    Cliente
}