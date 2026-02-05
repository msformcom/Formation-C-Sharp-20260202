namespace Metier;
public class ElementListe
{
    public ElementListe(string libele, int nombre=1) // nombre est optionel
    {
        this.Nombre=nombre;
        this.Libele = libele;
    }
    public bool Achete { get; internal  set; } = false;

    #region Libele
    private string _Libele;
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
    private int _Nombre;
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