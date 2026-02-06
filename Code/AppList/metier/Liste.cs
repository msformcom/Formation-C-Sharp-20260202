using System.Runtime.Serialization;

namespace Metier;

[DataContract]  // Déclarer que cette classe est sérialisable
public class Liste
{
    public Liste(string libele)
    {
        this.Libele = libele;
        this.MyElements = new();
    }

    #region Libele

    [DataMember(Name ="Label")] // Permet de customiser la serialisation
    private string _Libele;

    public string Libele
    {
        get { return _Libele; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("Libélé est obligatoire");
            }
            _Libele = value;
        }
    }
    #endregion

      [DataMember(Name ="Elements")]
    private List<ElementListe> MyElements { get; set; }

    public IEnumerable<ElementListe> Elements
    {
        get
        {
            // l est déclarée comme IEnumerable<ElementListe> => pas de Add
            // l contient une liste
            // en castant l en tant que List<ElementListe>
            // IEnumerable<ElementListe> l=MyElements;
            //((List<ElementListe>)l).Add

            // return MyElements;  Renvoit la liste dans une référence de type 
            foreach (var e in MyElements)
            {
                yield return e;
            }
        }
    }

    public void AddElement(ElementListe e)
    {
        if (MyElements.Count >= 10)
        {
            throw new InvalidOperationException("La liste est déjà pleine");
        }
        MyElements.Add(e);
    }
    public void RemoveElement(ElementListe e)
    {
        MyElements.Remove(e);
    }

    public void UpdateElement(ElementListe e1, ElementListe e2)
    {
        RemoveElement(e1);
        AddElement(e2);
    }

}