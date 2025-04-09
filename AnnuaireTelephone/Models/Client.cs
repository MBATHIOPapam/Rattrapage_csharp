public class Client
{
    public int Id { get; set; }
    public string Matricule { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }

    public ICollection<Telephone> Telephones { get; set; }
}
