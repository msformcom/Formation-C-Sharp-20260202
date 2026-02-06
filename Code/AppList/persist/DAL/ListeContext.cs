
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// cet objet va représenter la partie de la BDD que j'utilise
public  class ListeContext : DbContext
{
    // Pour que DI puisse construire un ListeContext avec les options de configuration
    // Le constructeur de ListeContext reçoit les options 
    // et les passe au constructeur de la base (DbContext)
    public ListeContext(DbContextOptions<ListeContext> options):base(options)
    {
        
    }
    // Permet d'ajouter à mon contexte le "support" des Listes
    public DbSet<ListeDAO> Listes { get; set; }
    public DbSet<ElementListeDAO> Elements { get; set; }

    // Permet de customiser la BDD lors de sa création
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ListeDAO>(options =>
        {
            options.HasKey(c=>c.Id);
            options.HasIndex(c=>c.Libele);
            options.Property(c=>c.Libele).HasMaxLength(50).IsRequired().IsUnicode();
            // Pas de mise à jour après création
            options.Property(c=>c.DateCreation).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
           
        });

    }
}