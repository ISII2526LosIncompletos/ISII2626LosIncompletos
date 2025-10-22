using ISII2626LosIncompletos.API.DTOs.OfertaDTOs;

namespace AppForSEII2526.API.DTOs.HerramientasDTOs
{
    namespace ISII2626LosIncompletos.API.DTOs.OfertaDTOs
    {
        public class HerramientasDTO
        {
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
}
