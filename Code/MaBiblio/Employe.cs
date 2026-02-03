namespace MaBiblio;
// Héritage => reprendre tous les éléménts de Personne
public class Employe : Personne
{
    public Employe(DateTime dateNaissance):this("Employe inconnu",dateNaissance)
    {
        
    }

    public Employe(string dateNaissance):this("Employe inconnu", new DateTime(1970,1,1))
    {
        this.DateNaissance=DateTime.Parse(dateNaissance);
    }
    public Employe(string nom, DateTime dateNaissance):base(nom,dateNaissance)
    {
        this.Salaire=1000;
    }
    public Decimal Salaire { get; set; }
}

