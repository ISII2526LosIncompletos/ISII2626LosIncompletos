namespace AppForSEII2526.API.Models
{

    public class Compra

    {
        public Compra(List<CompraItem> CompraItems)
        {
            CompraItems = new List<CompraItem>();
        }
        public Compra(DateTime fechaCompra, decimal precioTotal,tiposMetodosPago metodoPago, List<CompraItem> compraItems, ApplicationUser applicationUser )
        {
            FechaCompra = fechaCompra;
            PrecioTotal = precioTotal;
            MetodoPago = metodoPago;
            CompraItems = compraItems;
            ApplicationUser = applicationUser;


        }
        public int Id { get; set; }


        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Fecha de compra")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Precio total compra")]
        [Precision(5, 2)]
        public decimal PrecioTotal { get; set; }


        [Required]
        [Display(Name = "Tipos metodos de pago")]
        public tiposMetodosPago MetodoPago { get; set; }

        public List<CompraItem> CompraItems { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}