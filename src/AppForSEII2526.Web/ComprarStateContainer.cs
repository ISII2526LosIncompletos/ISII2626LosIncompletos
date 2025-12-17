using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class ComprarStateContainer
    {
        //Creamos una instancia de Compra cuando se crea una instancia de ComprarStateContainer
        public CompraCreacionDTO Compra { get; private set; } = new CompraCreacionDTO()
        {
            CompraItems = new List<CompraItemDTO>()
        };

        //Calculamos el PrecioTotal de Compra a partir de los items que tiene la compra
        public double PrecioTotal
        {
            get
            {
                return Compra.CompraItems.Sum(item => item.PrecioCompra);
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();



        public void AddHerramienta(HerramientasDTO herramienta)
        {
            //Antes de añadir una herramienta, comprobamos si ya esta en la lista
            if (!Compra.CompraItems.Any(ri => ri.Nombre == herramienta.Nombre))
                //La añadimos si no está en la lista
                Compra.CompraItems.Add(new CompraItemDTO()
                {
                    HerramientaID = herramienta.HerramientaID,
                    Nombre = herramienta.Nombre,
                    Material = herramienta.Material,
                    PrecioCompra = herramienta.Precio,
                    Cantidad = 1,
                    Descripcion = ""
                }
            );

        }

        //Para borrar herramientas del carrito de compra
        public void EliminarHerramienta(CompraItemDTO item)
        {
            Compra.CompraItems.Remove(item);

        }

        //Elimina todas las herramientas de la lista de compra
        public void EliminarTodasHerramientas()
        {
            Compra.CompraItems.Clear();

        }

        //Ya hemos terminado el proceso de compra, se crea una nueva compra
        public void FinalizarCompra()
        {
            //we have finished the rental process so we create a new object without data
            Compra = new CompraCreacionDTO()
            {
                CompraItems = new List<CompraItemDTO>()
            };
        }
    }
}
