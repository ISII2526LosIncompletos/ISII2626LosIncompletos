
namespace AppForSEII2526.API.Models
{
    public class Reparacion
    {

        public Reparacion()
        {
            ItemsReparacion = new List<ReparacionItem>();
        }

        public Reparacion(DateTime fechaEntrega, tiposMetodosPago metodoPago,
            List<ReparacionItem> itemsReparacion, ApplicationUser applicationUser)
        {
            FechaEntrega = fechaEntrega;
            MetodoPago = metodoPago;
            ItemsReparacion = itemsReparacion;
            ApplicationUser = applicationUser;
        }

        public Reparacion(DateTime fechaEntrega, DateTime fechaRecogida, decimal precioTotal,
            tiposMetodosPago metodoPago, List<ReparacionItem> itemsReparacion, ApplicationUser applicationUser)
        {
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            PrecioTotal = precioTotal;
            MetodoPago = metodoPago;
            ItemsReparacion = itemsReparacion;
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

        public List<ReparacionItem> ItemsReparacion { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}