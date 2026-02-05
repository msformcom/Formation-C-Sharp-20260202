using Metier;

namespace persist;


//Interface Générique 
// Certains types peuvent varier
// A l'utilisation IPersist<Guid,Liste> a;
public interface IPersist<TKey, TObjet, TSearch>
{
    // Comme je ne sais pas en quel temps va être réalisée cette opération
    // Elle renvoit un Task
    public Task<IEnumerable<(TKey Id, TSearch search)>> SearchAsync(string texte);
    public Task<TObjet> GetAsync(TKey id);
    public Task<TObjet> AddAsync(TObjet o, TKey? id);
    public Task<TObjet> UpdateAsync(TKey id, TObjet o);
    public Task RemoveAsync(TKey id);


}
