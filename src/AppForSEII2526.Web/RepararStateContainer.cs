using AppForSEII2526.API.DTOs.HerramientaDTOs;
using AppForSEII2526.API.DTOs.ReparacionDTOs;
using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class RepararStateContainer
    {

        //Creamos una instancia de Reparacion cuando se crea una instancia de RepararStateContainer
        public ReparacionCreacionDTO Reparacion { get; private set; } = new ReparacionCreacionDTO()
        {
            ItemsReparacion = new List<ReparacionItemDTO>()
        };

        //Calculamos el precio total de la reparacion seleccionada
        public decimal PrecioTotal
        {
            get
            {
                int recogida = (Reparacion.ItemsReparacion.Sum(r => r.TiempoReparacion));
                int numDias = (Reparacion.FechaEntrega.AddDays(recogida) - Reparacion.FechaEntrega).Days;
                return Convert.ToDecimal(Reparacion.ItemsReparacion.Sum(ri => ri.PrecioReparacion * numDias));
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();


        public void AddHerramientaParaReparar(HerramientasDTO herramienta)
        {
            //Antes de añadir una herramienta, verificamos si ya está añadida.
            if (!Reparacion.ItemsReparacion.Any(ri => ri.HerramientaID == herramienta.HerramientaID))
                //La añadimos si no está ya en la lista
                Reparacion.ItemsReparacion.Add(new ReparacionItemDTO(herramienta.HerramientaID, herramienta.Nombre,
                    herramienta.Precio, herramienta.TiempoReparacion, herramienta.Cantidad, herramienta.Descripcion)
                );
        }

        //Para borrar una herramienta de la lista de herramientas seleccionadas
        public void RemoveReparacionItem(ReparacionItemDTO item)
        {
            Reparacion.ItemsReparacion.Remove(item);
        }

        //Eliminamos todas las herramientas de la lista
        public void ClearRentingCart()
        {
            Reparacion.ItemsReparacion.Clear();
        }

        //Cuando finalizamos el proceso de reparación, creamos una Reparación
        public void ReparacionProcessed()
        {
            //Hemos finalizado el proceso de reparación, creamos un nuevo objeto sin datos
            Reparacion = new ReparacionCreacionDTO()
            {
                ItemsReparacion = new List<ReparacionItemDTO>()
            };
        }

    }
}
