namespace MaBiblio;

// Modificateurs d'accès 
// Par defaut : internal 
// public 
public class Personne
{
    public Personne(DateTime dateNaissance) : this("Personne inconnue", dateNaissance)
    {

    }

    // Constructeur = code exécuté lors de la construction
    public Personne(string nom, DateTime dateNaissance)
    {
        this.Nom = nom;
        this.DateNaissance = dateNaissance;
        NbPersonnes++;
        this.DateCreation = DateTime.Now;
    }

    // Destructeur => Méthode exécutée par le Garbage collector au moment de son retrait de la RAM
    ~Personne()
    {
        NbPersonnes--;
    }

    // Date naissance minimale acceptée pour l'ensemble des instances de cette classe
    private static DateTime DateNaissanceMinimale = new DateTime(1970, 1, 1);

    private DateTime DateCreation;
    public static int NbPersonnes = 0;
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


    private DateTime _DateNaissance;
    public DateTime DateNaissance
    {
        get
        {
            return this._DateNaissance;
        }
        protected set
        {
            if (value < DateNaissanceMinimale || value > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException($"La date de naissance doit être entre le {DateNaissanceMinimale:dd/MM/yyyy} et aujourd'hui");
            }
            this._DateNaissance = value;
        }
    }


    // Propriété => permet d'écrire un getter et un setter pour cette valeur
    //private int _Age;
    public int Age
    {
        get
        {
            var today = DateTime.Today;

            // Calculate the age.
            var age = today.Year - this.DateNaissance.Year;

            // If the birthdate hasn't arrived yet, subtract one year.
            if (this.DateNaissance.Date > today.AddYears(-age)) age--;
            return age;

        }
        // set
        // {
        //     // Condition pour accepter la valeur
        //     if (value < AgeMinimum || value > AgeMaximum)
        //     {
        //         var a = string.Format("La date est {0:dd/MM/yyyy}", DateTime.Now);
        //         throw new Exception(string.Format("L'age doit être entre {0} et {1}", AgeMinimum, AgeMaximum));

        //         //throw new ArgumentOutOfRangeException($"L'age doit être entre {AgeMinimum} et {AgeMaximum}");
        //     }
        //     _Age = value;
        // }
    }


}
