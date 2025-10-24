namespace AppForSEII2526.API.Models { 

    public class Fabricante
    {
        public int Id { get; set; }
    
        [Required]
        [StringLength(20, ErrorMessage = "Nombre no puede superar los 20 caracteres.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string Nombre { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public List<Herramienta> Herramienta { get; set; }
    }
}
