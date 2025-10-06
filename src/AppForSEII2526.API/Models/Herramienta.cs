using System;


   
    public class Herramienta
    {
        public Herramienta()
        {
        }

        public Herramienta(string material,int nombre,double precio,string fabricante)
        {
        Material = material;
        Nombre = nombre;
        Precio = precio;
        Fabricante = fabricante;
        }

        public int Id { get; set; }


        [StringLength(50, ErrorMessage = "El material no puede tener mas de 30 caracteres")]
        public string Material { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.5, float.MaxValue, ErrorMessage = "El precioMinimo es 1 ")]
        [Display(Name = "Precio")]
        [Precision(10, 2)]
        public decimal Precio { get; set; }


        [Display(Name = "Nombre")]
        [Range(0, int.MaxValue, ErrorMessage = "Error nombre")]
        public int Nombre { get; set; }

        [StringLength(50, ErrorMessage = "El fabricante no puede tener mas de 30 caracteres")]
        public string Fabricante { get; set; }

        public IList<AlquilarItem> AlquilarItems { get; set; }
        public IList<CompraItem> CompraItems { get; set; }
        public IList<OfertaItem> OfertaItems { get; set; }
        public IList<ItemReparacion> ItemsReparacion { get; set; }

}
