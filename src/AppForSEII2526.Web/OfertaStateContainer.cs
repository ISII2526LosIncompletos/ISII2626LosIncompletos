using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class OfertaStateContainer
    {
        public OfertaForCreationDTO Oferta { get; private set; } = new OfertaForCreationDTO()
        {
            Items = new List<OfertaItemDTO>()
        };

        public float PrecioFinal
        {
            get
            {
                return (float)Oferta.Items.Sum(oi => oi.PrecioFinal);
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();


        public void AddHerramientaToOferta(HerramientasDTO herramienta)
        {
            if (!Oferta.Items.Any(oi => oi.HerramientaId == herramienta.HerramientaID))
                Oferta.Items.Add(new OfertaItemDTO()
                {
                    HerramientaId = herramienta.HerramientaID,
                    HerramientaNombre = herramienta.Nombre,
                    HerramientaMaterial = herramienta.Material,
                    PrecioOriginal = herramienta.Precio,
                    FabricanteNombre = herramienta.Fabricante,
                }
            );
        }

        public void RemoveOfertaItemToOferta(OfertaItemDTO item)
        {
            Oferta.Items.Remove(item);
        }

        public void ClearOfertaCart()
        {
            Oferta.Items.Clear();
        }

        public void OfertaProcessed()
        {
            Oferta = new OfertaForCreationDTO()
            {
                Items = new List<OfertaItemDTO>()
            };
        }
    }
}