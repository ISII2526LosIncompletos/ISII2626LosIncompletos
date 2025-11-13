namespace ISII2626LosIncompletos.API.DTOs.OfertaDTOs
{
    public class OfertarHerramientasDetalleDTOs
    {
        public OfertarHerramientasDetalleDTOs(string herramientaNombre, string herramientaMaterial, string fabricanteNombre,
                                             double precioOriginal, double precioFinal, double porcentaje)
        {
            HerramientaNombre = herramientaNombre;
            HerramientaMaterial = herramientaMaterial;
            FabricanteNombre = fabricanteNombre;
            PrecioOriginal = precioOriginal;
            PrecioFinal = precioFinal;
            Porcentaje = porcentaje;
        }

        [Required]
        public string HerramientaNombre { get; set; } 

        public string HerramientaMaterial { get; set; } 

        public string FabricanteNombre { get; set; } 

        [Required]
        public double PrecioOriginal { get; set; } 

        [Required]
        public double PrecioFinal { get; set; } 

        [Required]
        public double Porcentaje { get; set; } 
    }
}
