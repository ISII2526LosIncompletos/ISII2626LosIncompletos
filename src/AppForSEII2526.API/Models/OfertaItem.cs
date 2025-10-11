﻿namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(IdOferta), nameof(IdHerramienta))]
    public class OfertaItem
    {
        public Herramienta Herramienta { get; set; }
        public int IdHerramienta { get; set; }
        public Oferta Oferta { get; set; }
        public int IdOferta { get; set; }


        [Range(0, 100, ErrorMessage = "El porcentaje estara entre 0 y 100")]
        public int Porcentaje { get; set; }

        [Range(0.05, float.MaxValue, ErrorMessage = "El precio minimo es 0.05")]
        [Precision(10, 2)]
        public decimal PrecioFinal { get; set; }
    }
}

