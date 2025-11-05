
namespace AppForSEII2526.API.Models
{
    public class Reparacion
    {

        public Reparacion()
        {
            ReparacionItem = new List<ReparacionItem>();
        }

        public Reparacion(DateTime fechaEntrega, DateTime fechaRecogida, decimal precioTotal, 
            tiposMetodosPago metodoPago, List<ReparacionItem> reparacionItem, ApplicationUser applicationUser)
        {
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            PrecioTotal = precioTotal;
            MetodoPago = metodoPago;
            ReparacionItem = reparacionItem;
            ApplicationUser = applicationUser;
        }

        public int Id { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaEntrega { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaRecogida { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Precio total reparación")]
        [Precision(5, 2)]
        public decimal PrecioTotal { get; set; }

        [Required]
        [Display(Name = "Metodos de pago")]
        public tiposMetodosPago MetodoPago { get; set; }

        public List<ReparacionItem> ReparacionItem { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

    }
}