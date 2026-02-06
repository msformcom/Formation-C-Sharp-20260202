// Cette classe sert à structurer et accéder aux données d' une table dans la BDD
public  class ElementListeDAO
{
    public Guid Id { get; set; }= Guid.NewGuid();

    public Guid IdListe { get; set; }

    // Propriété de navigation (coté 1) vers la Liste associée
    public ListeDAO Liste { get; set; }


    public bool Achete { get; set; }
    public string Libele { get; set; }
    public int Nombre { get; set; }
    public DateTime DateCreation { get; set; }=DateTime.Now;
}