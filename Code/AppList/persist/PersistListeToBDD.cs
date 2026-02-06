using Metier;
using persist;

public class PersistListeToBDD : IPersist<Guid, Liste, string>
{
    public Task<Liste> AddAsync(Liste o, Guid id)
    {
        throw new NotImplementedException();
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