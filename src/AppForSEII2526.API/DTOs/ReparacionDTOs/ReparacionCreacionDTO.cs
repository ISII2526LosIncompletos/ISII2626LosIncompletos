using AppForSEII2526.API.Models;
using AppForSEII2526.API.DTOs.HerramientaDTOs;

namespace AppForSEII2526.API.DTOs.ReparacionDTOs
{
    public class ReparacionCreacionDTO
    {
        public ReparacionCreacionDTO(string nombreCliente, string apellidoCliente, string? numTelefono,
            DateTime fechaEntrega, tiposMetodosPago metodoPago, IList<ReparacionItemDTO> itemsReparacion)
        {
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            NumTelefono = numTelefono;
            FechaEntrega = fechaEntrega;
            MetodoPago = metodoPago;
            ItemsReparacion = itemsReparacion;
        }

        public ReparacionCreacionDTO()
        {
            ItemsReparacion = new List<ReparacionItemDTO>();
        }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(20, ErrorMessage = "Nombre no puede superar los 20 caracteres.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string NombreCliente { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        [StringLength(40, ErrorMessage = "Apellidos no puede superar los 40 caracteres.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string ApellidoCliente { get; set; }

        [Display(Name = "Número de teléfono")]
        [StringLength(9, ErrorMessage = "Número de telefono no puede superar los 9 caracteres.")]
        [RegularExpression(@"^[0-9]*$")]
        public string? NumTelefono { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaEntrega { get; set; }

        [Required]
        public tiposMetodosPago MetodoPago { get; set; }

        [Required]
        public IList<ReparacionItemDTO> ItemsReparacion { get; set; }

        protected bool CompararFechas(DateTime fecha1, DateTime fecha2)
        {
            return (fecha1.Subtract(fecha2) < new TimeSpan(0, 1, 0));
        }

        public override bool Equals(object? obj)
        {
            return obj is ReparacionCreacionDTO dTO &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidoCliente == dTO.ApellidoCliente &&
                   FechaEntrega == dTO.FechaEntrega &&
                   MetodoPago == dTO.MetodoPago &&
                   NumTelefono == dTO.NumTelefono &&
                   ItemsReparacion.SequenceEqual(dTO.ItemsReparacion);
        }

    }
}