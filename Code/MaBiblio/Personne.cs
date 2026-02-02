namespace MaBiblio;

// Modificateurs d'accès 
// Par defaut : internal 
// public 
public class Personne
{
    public Personne(): this("Inconnu")
    {
        
    }

    // Constructeur = code exécuté lors de la construction
    public Personne(string nom)
    {
        this.Nom=nom;
        NbPersonnes++;
        this.DateCreation=DateTime.Now;
    }

    // Destructeur => Méthode exécutée par le Garbage collector au moment de son retrait de la RAM
    ~Personne()
    {
        NbPersonnes--;
    }

    private DateTime  DateCreation;
    public static int  NbPersonnes=0;
    public static readonly int AgeMinimum = 0;
    public static readonly int AgeMaximum = 200;

    // Champs => Stocke une information
    // Modificateurs d'accès sur membre d'un classe
    // private => Visible à l'intérieur de la class
    // public => Partout
    // protected => Visible dans la classe +  par les classes qui héritent de cette classe
    // internal => Visible dans l'assembly courante + projets référencés par InternalVisibleTo (csproj)
    public string ChampsPublic;

    private string ChampsPrive;
    protected string ChampsProtected;
    internal string ChampsInternal;

    // Champs => accepte toute valeur du type
    private string _Nom;
    public string Nom
    {
        // Les get et set sont facultatifs ou peuvent avoir une accessibilité différente (protected, internal) 
        get { return _Nom; } 
         set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("Le nom doit être fourni");
            }
            _Nom = value;
        }
    }


    // Propriété => permet d'écrire un getter et un setter pour cette valeur
    private int _Age;
    public int Age
    {
        get { return _Age; }
        set
        {
            // Condition pour accepter la valeur
            if (value < AgeMinimum || value > AgeMaximum)
            {
                var a = string.Format("La date est {0:dd/MM/yyyy}", DateTime.Now);
                throw new Exception(string.Format("L'age doit être entre {0} et {1}", AgeMinimum, AgeMaximum));

                //throw new ArgumentOutOfRangeException($"L'age doit être entre {AgeMinimum} et {AgeMaximum}");
            }
            _Age = value;
        }
    }


}
