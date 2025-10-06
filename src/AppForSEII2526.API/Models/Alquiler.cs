using System;


{
    public class Alquiler
    {
       
        

        

    public int Id { get; set; }
    public IList<AlquilarItem> AlquilarItems { get; set; }

    [StringLength(50, ErrorMessage = "El correo no puede tener mas de 50 caracteres")] 
    public string Correo { get; set; }
    public double PrecioTotal { get; set; }

        public DateTime FechaAlquiler { get; set; }

        public DateTime FechaFin { get; set; }
        public DateTime FechaInicio { get; set; }
        [StringLength(9, ErrorMessage = "El numero de telefono no puede tener mas de 9 caracteres.")] 
        public string NumeroTelefono { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Direccion de envio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Introduce la direccion de envio")]
        public string DireccionEnvio { get; set; }

        [StringLength(50, ErrorMessage = "El apellido no puede tener mas de 50 caracteres")] 
        public string ApellidoCliente { get; set; }
        [StringLength(50, ErrorMessage = "El nombre no puede tener mas de 50 caracteres")]
        public string NombreCliente { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethodTypes MetodoPagos { get; set; }
        public ApplicationUser ApplicationUser { get; set; }




}

    public enum MetodosPago
    {
        TargetaCredito,
        PayPal,
        Efectivo
    }
}
