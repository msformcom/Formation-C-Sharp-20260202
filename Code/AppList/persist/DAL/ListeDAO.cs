using System.Collections.ObjectModel;

// Cette classe sert à structurer et accéder aux données d' une table dans la BDD
public class ListeDAO
{
    public ListeDAO()
    {
        
    }
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Libele { get; set; }

    public DateTime DateCreation { get; set; } = DateTime.Now;

    // Ajouter une propriété de navigation (coté n)
    // IEnumerable => Count, where
    // Remove, Add, Contains
    public ICollection<ElementListeDAO> Elements { get; set; }
}