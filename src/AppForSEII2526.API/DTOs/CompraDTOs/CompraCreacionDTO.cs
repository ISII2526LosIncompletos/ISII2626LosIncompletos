using AppForSEII2526.API.Models;
using AppForSEII2526.API.DTOs.HerramientaDTOs;

namespace AppForSEII2526.API.DTOs.CompraDTOs
{
    public class CompraCreacionDTO
    {
        public CompraCreacionDTO(string nombreCliente, string apellidoCliente, string direccionEnvio, 
            tiposMetodosPago metodoPago, string numTelefono, DateTime fechaCompra, IList<CompraItemDTO> compraItems)
        {
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            DireccionEnvio = direccionEnvio;
            MetodoPago = metodoPago;
            NumTelefono = numTelefono;
            FechaCompra = fechaCompra;
            CompraItems = compraItems;
        }
        public CompraCreacionDTO()
        {
            CompraItems = new List<CompraItemDTO>();
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

        [Required]
        [Display(Name = "Direccion de envío")]
        [StringLength(50, ErrorMessage = "Apellidos no puede superar los 50 caracteres.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string DireccionEnvio { get; set; }
        [Required]
        public tiposMetodosPago MetodoPago { get; set; }

        [Required]
        [Display(Name = "Número de teléfono")]
        [StringLength(9, ErrorMessage = "Número de teléfono no puede superar los 9 números.")]
        [RegularExpression(@"^[0-9]]*$")]
        public string NumTelefono { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name ="Fecha de Compra")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra { get; set; }

        [Required]
        public IList<CompraItemDTO> CompraItems {  get; set; }

    }
}
