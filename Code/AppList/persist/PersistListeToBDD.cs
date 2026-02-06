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
  

        // var dao=mapper.Map<ListeDAO>(liste);
          db.Listes.Add(listeAAjouter);
          await db.SaveChangesAsync();
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

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<(Guid Id, string search)>> SearchAsync(string texte)
    {
        throw new NotImplementedException();
    }

    public Task<Liste> UpdateAsync(Guid id, Liste o)
    {
        throw new NotImplementedException();
    }
}