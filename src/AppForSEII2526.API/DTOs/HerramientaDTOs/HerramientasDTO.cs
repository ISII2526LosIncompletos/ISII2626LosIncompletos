using ISII2626LosIncompletos.API.DTOs.OfertaDTOs;

namespace AppForSEII2526.API.DTOs.HerramientaDTOs
{
    public class HerramientasDTO
    {
        public HerramientasDTO(int herramientaID, string nombre, string material, string fabricante, decimal precio, DateTime tiempoReparacion)
        {
            HerramientaID = herramientaID;
            Nombre = nombre;
            Material = material;
            Fabricante = fabricante;
            Precio = precio;
            TiempoReparacion = tiempoReparacion;
        }

        public int HerramientaID { get; set; }
        public string Nombre { get; set; }
        public string Material { get; set; }
        public string Fabricante { get; set; }
        public decimal Precio { get; set; }
        public DateTime TiempoReparacion { get; set; }
        public string? Descripcion { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is HerramientasDTO dTO &&
                   HerramientaID == dTO.HerramientaID &&
                   Nombre == dTO.Nombre &&
                   Material == dTO.Material &&
                   Fabricante == dTO.Fabricante &&
                   Precio == dTO.Precio &&
                   TiempoReparacion == dTO.TiempoReparacion;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HerramientaID, Nombre, Material, Fabricante, Precio, TiempoReparacion);
        }
        private object id;
        private object fechaInicio;
        private object fechaFinal;
        private object fechaOferta;
        private object metodoPago;
        private object dirigidaA;

        public HerramientasDTO(object id, object fechaInicio, object fechaFinal, object fechaOferta, object metodoPago, object dirigidaA)
        {
            this.id = id;
            this.fechaInicio = fechaInicio;
            this.fechaFinal = fechaFinal;
            this.fechaOferta = fechaOferta;
            this.metodoPago = metodoPago;
            this.dirigidaA = dirigidaA;
        }

        public int Id { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFinal { get; set; }

        public DateTime FechaOferta { get; set; }

        [Required]
        public string MetodoPago { get; set; }

        public string DirigidaA { get; set; }

        public IList<OfertarHerramientasDetalleDTOs> Items { get; set; }
    }
}
