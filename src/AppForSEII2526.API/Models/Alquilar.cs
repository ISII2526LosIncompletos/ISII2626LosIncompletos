using System;

public class Alquilar
{
    public Alquilar()
    {
    }

    public Alquilar(string apellidoCliente, string correo, string direccionEnvio, DateTime fechaAlquiler, DateTime fechaFin, string nombreCliente, string numeroTelefono, int periodo, double precioTotal, MetodosPago metodosPago)
    {

        ApellidoCliente = apellidoCliente;
        Correo = correo;
        DireccionEnvio = direccionEnvio;
        FechaAlquiler = fechaAlquiler;
        FechaFin = fechaFin;
        NombreCliente = nombreCliente;
        NumeroTelefono = numeroTelefono;
        Periodo = periodo;
        PrecioTotal = precioTotal;
        MetodosPago = metodosPago;

    }

    public int ID { get; set; }
    public string ApellidoCliente { get; set; }
    public string Correo { get; set; }
    public DateTime FechaAlquiler { get; set; }
    public DateTime FechaFin { get; set; }
    public string NombreCliente { get; set; }
    public string NumeroTelefono { get; set; }
    public int Periodo { get; set; }
    public double PrecioTotal { get; set; }
    public MetodosPago MetodosPago { get; set; }

    [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
    [Display(Name = "Direccion Envio")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Introduce la direccion de envio")]
    public string DeliveryAddress { get; set; }

    public enum MetodosPago
    {
        TarjetaCredito,
        PayPal,
        Efectivo

    }


}
