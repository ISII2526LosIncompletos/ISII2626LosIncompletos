using System;

public class Fabricante
{
    [Key]
    public int id { get; set; }

    [StringLength(25, ErrorMessage = "Name can be neither longer than 25 characters nor shorter than 1.“, MinimumLength=1)]
    [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string nombre { get; set; }

    public ApplicationUser ApplicationUser { get; set; }

    List<Herramienta> Herramienta { get; set; }

}