using System;


    public class AlquilarItem
    {
        public AlquilarItem()
        {
        }

        public AlquilarItem(int cantidad,double precio,int idAlquiler,int idHerramienta)
        {
        Cantidad = cantidad;
        Precio = precio;
        IdAlquiler = idAlquiler;
        IdHerramienta=idHerramienta
        }


    public int Cantidad { get; set; }

    public IdAlquiler { get; set; }

    public int IdHerramienta { get; set; }
    
    public double Precio { get; set; }
    }