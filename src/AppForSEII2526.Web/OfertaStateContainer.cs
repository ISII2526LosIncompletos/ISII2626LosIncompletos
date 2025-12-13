using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class OfertaStateContainer
    {
        public OfertaForCreationDTO Oferta { get; private set; } = new OfertaForCreationDTO()
        {
            Items = new List<OfertaItemInput>()
        };

        public List<OfertaItemDTO> OfertaItems { get; private set; } = new List<OfertaItemDTO>();

        public float PrecioFinal
        {
            get
            {
                return (float)OfertaItems.Sum(oi => oi.PrecioFinal);
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddHerramientaToOferta(HerramientasDTO herramienta)
        {
            if (!OfertaItems.Any(oi => oi.HerramientaId == herramienta.HerramientaID))
            {
                var itemDto = new OfertaItemDTO()
                {
                    HerramientaId = herramienta.HerramientaID,
                    HerramientaNombre = herramienta.Nombre,
                    HerramientaMaterial = herramienta.Material,
                    PrecioOriginal = herramienta.Precio,
                    FabricanteNombre = herramienta.Fabricante,
                };
                OfertaItems.Add(itemDto);
                Oferta.Items.Add(new OfertaItemInput
                {
                    HerramientaId = herramienta.HerramientaID,
                    Porcentaje = itemDto.Porcentaje
                });
            }
        }

        public void RemoveOfertaItemToOferta(OfertaItemDTO item)
        {
            OfertaItems.Remove(item);
            var inputItem = Oferta.Items.FirstOrDefault(i => i.HerramientaId == item.HerramientaId);
            if (inputItem != null)
            {
                Oferta.Items.Remove(inputItem);
            }
        }

        public void ClearOfertaCart()
        {
            OfertaItems.Clear();
            Oferta.Items.Clear();
        }

        public void OfertaProcessed()
        {
            Oferta = new OfertaForCreationDTO()
            {
                Items = new List<OfertaItemInput>()
            };
            OfertaItems.Clear();
        }
    }
}