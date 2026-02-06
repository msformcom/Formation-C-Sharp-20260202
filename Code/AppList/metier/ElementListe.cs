using System.Runtime.Serialization;

namespace Metier;

[DataContract]
public class ElementListe
{
    internal ElementListe(){}
    public ElementListe(string libele, int nombre = 1) // nombre est optionel
    {
        this.Nombre = nombre;
        this.Libele = libele;
    }

    [DataMember(Name = "Done")]
    #region Achete
    internal bool _Achete;
    public bool Achete
    {
        get { return _Achete; }
        set { 
    // TODO Check value
    _Achete = value; }
    }
    #endregion
    

    #region Libele
    [DataMember(Name = "Label")]
    internal string _Libele;
    public string Libele
    {
        get { return _Libele; }
        internal set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Libelé doit être non vide");
            }
            _Libele = value;
        }
    }
    #endregion

    #region Nombre
    
    [DataMember(Name = "Number")]
    internal int _Nombre;
    public int Nombre
    {
        get { return _Nombre; }
        internal set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Nombre doit être supérieur à zéro");
            }
            _Nombre = value;
        }
    }
    #endregion

}