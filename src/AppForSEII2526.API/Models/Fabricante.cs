namespace AppForSEII2526.API.Models {

    public class Fabricante
    {
        public Fabricante()
        {
            Herramienta = new List<Herramienta>();
        }

        public Fabricante(string nombre, List<Herramienta> herramienta)
        {
            Nombre = nombre;
            Herramienta = herramienta;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Nombre no puede superar los 20 caracteres.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string Nombre { get; set; }

        public List<Herramienta> Herramienta { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Fabricante fabricante &&
                   Id == fabricante.Id &&
                   Nombre == fabricante.Nombre &&
                   Herramienta.SequenceEqual(fabricante.Herramienta);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre, Herramienta);
        }
    }
}