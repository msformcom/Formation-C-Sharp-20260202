// Cette classe sert à structurer et accéder aux données d' une table dans la BDD
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TBL_Elements")]
public class ElementListeDAO
{
    [Key]
    [Column("PK_Elements")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [ForeignKey("Liste")]
    [Column("FK_Liste")]
    public Guid IdListe { get; set; }

    // Propriété de navigation (coté 1) vers la Liste associée
    public ListeDAO Liste { get; set; }


    public bool Achete { get; set; }


    [MaxLength(50)]
    public string Libele { get; set; }
    public int Nombre { get; set; }
    public DateTime DateCreation { get; set; } = DateTime.Now;
}