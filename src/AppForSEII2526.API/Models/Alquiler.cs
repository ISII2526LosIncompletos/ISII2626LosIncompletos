using System;


{
    public class Alquiler
    {
        public Alquiler()
        {
        }
        public Alquiler(string apellidoCliente, string correo, string direccionEnvio,DateTime fechaAlquiler,DateTime fechaFin,DateTime fechaInicio,string nombreCliente,string numeroTelefono,int periodo,double precioTotal,PaymentMethodTypes metodoPagos)
        {
        ApellidoCliente = apellidoCliente;
        Correo = correo;
        DireccionEnvio = direccionEnvio;
        FechaAlquiler = fechaAlquiler;
        FechaFin = fechaFin;
        FechaInicio = fechaInicio;
        NombreCliente = nombreCliente;
        NumeroTelefono = numeroTelefono;
        Periodo = periodo;
        PrecioTotal = precioTotal;
        MetodoPagos = metodoPagos;

        }

    public int Id { get; set; }
    public IList<AlquilarItem> AlquilarItems { get; set; }
   
   

    public double PrecioTotal { get; set; }

        public DateTime FechaAlquiler { get; set; }

        public DateTime FechaFin { get; set; }
        public DateTime FechaInicio { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Direccion de envio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Introduce la direccion de envio")]
        public string DireccionEnvio { get; set; }

        public string ApellidoCliente { get; set; }

        public string NombreCliente { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethodTypes MetodoPagos { get; set; }

        

        
    }

    public enum MetodosPago
    {
        TargetaCredito,
        PayPal,
        Efectivo
    }
}
