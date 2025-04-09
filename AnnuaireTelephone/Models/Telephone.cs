public class Telephone
{
    public int Id { get; set; }
    public string Numero { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }

    public string Operateur => GetOperateur();

    private string GetOperateur()
    {
        if (Numero.StartsWith("77") || Numero.StartsWith("78")) return "Orange";
        if (Numero.StartsWith("76")) return "Yas";
        if (Numero.StartsWith("70")) return "Expresso";
        return "Inconnu";
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Telephone
{
    public int Id { get; set; }

    [Required]
    [StringLength(9, MinimumLength = 9, ErrorMessage = "Le numéro doit contenir exactement 9 chiffres.")]
    [RegularExpression(@"^7[05678]\d{7}$", ErrorMessage = "Numéro invalide ou opérateur inconnu.")]
    public string Numero { get; set; }

    public string? Operateur
    {
        get
        {
            if (string.IsNullOrEmpty(Numero)) return null;
            return Numero switch
            {
                var n when n.StartsWith("77") || n.StartsWith("78") => "Orange",
                var n when n.StartsWith("76") => "Tigo",
                var n when n.StartsWith("70") => "Expresso",
                var n when n.StartsWith("75") => "Free",
                _ => "Inconnu"
            };
        }
    }

    public int ClientId { get; set; }
    public Client? Client { get; set; }
}
