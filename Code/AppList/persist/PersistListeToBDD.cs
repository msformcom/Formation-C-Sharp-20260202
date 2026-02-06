using Metier;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using persist;

public class PersistListeToBDD : IPersist<Guid, Liste, string>
{
    private readonly ListeContext db;
    private readonly ILogger<PersistListeToBDD> logger;


    // On a besoin d'un ListeContext bien configuré
    // On le demande à l'injection de dépeance
    public PersistListeToBDD(ListeContext db, ILogger<PersistListeToBDD> logger)
    {
        this.db = db;
        this.logger = logger;
    }
    public async Task<Liste> AddAsync(Liste o, Guid id)
    {
        var listeAAjouter=new ListeDAO();
        listeAAjouter.Id=id;
        listeAAjouter.Libele=o.Libele;
        // var dao=mapper.Map<ListeDAO>(liste);
          db.Listes.Add(listeAAjouter);
          await db.SaveChangesAsync();
          return o;
    }

    public Task<Liste> GetAsync(Guid id)
    {
        throw new NotImplementedException();
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