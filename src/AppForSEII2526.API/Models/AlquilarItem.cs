using System;

[PrimaryKey(nameof(HerramientaId),
nameof(IdAlquiler))]
public class AlquilarItem
    {
        

        

    [PrimaryKey(nameof(HerramientaId),
    nameof(IdAlquiler))]

    public int Cantidad { get; set; }

    public IdAlquiler { get; set; }
    public Alquiler Alquiler { get; set; }
    public Herramienta herramienta { get; set; }
    public int IdHerramienta { get; set; }
    
    public double Precio { get; set; }
    }