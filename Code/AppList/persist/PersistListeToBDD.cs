using System.Data.Common;
using AutoMapper;
using Metier;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using persist;

public class PersistListeToBDD : IPersist<Guid, Liste, string>
{
    private readonly ListeContext db;
    private readonly IMapper mapper;
    private readonly ILogger<PersistListeToBDD> logger;


    // On a besoin d'un ListeContext bien configuré
    // On le demande à l'injection de dépeance
    public PersistListeToBDD(ListeContext db, 
                                IMapper mapper,
                                ILogger<PersistListeToBDD> logger)
    {
        this.db = db;
        this.mapper = mapper;
        this.logger = logger;
    }
    public async Task<Liste> AddAsync(Liste o, Guid id)
    {
        // Cette instruction créé la BDD si elle n'existe pas
        db.Database.EnsureCreated();

        var listeAAjouter=new ListeDAO();
        listeAAjouter.Id=id;
        listeAAjouter.Libele=o.Libele;



        foreach(var dao in o.Elements
                    .Select(e=>new ElementListeDAO(){Achete=e.Achete, Libele=e.Libele,Nombre=e.Nombre }))
        {
            listeAAjouter.Elements.Add(dao);
        }
  
        var changesToContext=db.ChangeTracker.Entries().ToList();

        // var dao=mapper.Map<ListeDAO>(liste);
          db.Listes.Add(listeAAjouter);
          changesToContext=db.ChangeTracker.Entries().ToList();
          await db.SaveChangesAsync();
          //changesToContext=db.ChangeTracker.Entries().ToList();

        //listeAAjouter.Libele="Tata";
        // listeAAjouter est marqué comme Modified
        //   changesToContext=db.ChangeTracker.Entries().ToList();
        //   await db.SaveChangesAsync();
        //   changesToContext=db.ChangeTracker.Entries().ToList();
          return o;
    }

    public async Task<Liste> GetAsync(Guid id)
    {
        // recherche de la ListeDAO avec la méthode asynchone
        var dao=await db.Listes.FirstOrDefaultAsync(c=>c.Id==id);
        if (dao == null)
        {
            throw new KeyNotFoundException();
        }
        // Transformation en Liste
        //var pocoElement=mapper.Map<ElementListe>(dao.Elements.First());

        var poco=mapper.Map<Liste>(dao);
        return poco;

    }

    public async Task RemoveAsync(Guid id)
    {
        // // recherche de la ListeDAO avec la méthode asynchone
        // var dao=await db.Listes.FirstOrDefaultAsync(c=>c.Id==id);
        // if (dao == null)
        // {
        //     throw new KeyNotFoundException();
        // }
        // db.Listes.Remove(dao);

        // Création d'une entité avec l'Id à supprimer
        var dao=new ListeDAO(){Id=id};
        // Ajoyut au ChangeTracket avexc etat Deleted
        db.Entry(dao).State=EntityState.Deleted;
        // Le SaveChanges envoit le delete
        await  db.SaveChangesAsync();
    }

    public Task<IEnumerable<(Guid , string )>> SearchAsync(string texte)
    {
          var daos=db.Listes.Where(c=>c.Libele.Contains(texte)).AsEnumerable();
          return Task.FromResult(daos.Select(c=>(c.Id,c.Libele)));
    }

    public async Task<Liste> UpdateAsync(Guid id, Liste o)
    {
        // recherche de la ListeDAO avec la méthode asynchone
        // var dao=await db.Listes.FirstOrDefaultAsync(c=>c.Id==id);
        // if (dao == null)
        // {
        //     throw new KeyNotFoundException();
        // }
        // // Injecter les données du Poco dans le Dao
        // mapper.Map(o,dao);

        var dao=mapper.Map<ListeDAO>(o);
        dao.Id=id;
        db.ChangeTracker.Clear();
        db.Entry(dao).State=EntityState.Modified;
        // le dao avec les nouvelles valeurs injectées à partir de o
        // est marqué comme modified => db.SavecChanges va envoyer un update
        await db.SaveChangesAsync();
        return o;
    }
}