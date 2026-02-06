
using Microsoft.EntityFrameworkCore;

// cet objet va représenter la partie de la BDD que j'utilise
public  class ListeContext : DbContext
{
    // Permet d'ajouter à mon contexte le "support" des Listes
    public DbSet<ListeDAO> Listes { get; set; }
    public DbSet<ElementListeDAO> Elements { get; set; }
}