// Chien implémente IAnimalDomestique
class Chien : Vertebre, IAnimalDomestique, IVendable
{
    public string Nom { get ; set ; }
    public  override double Poids
    {
        get => base.Poids;
        set
        {
            if (value > 200)
            {
                throw new Exception("C'est un ours");
            }
            base.Poids = value;
        }
    }

    public decimal Prix { get; set ; }

    public void Vendre()
    {
        throw new NotImplementedException();
    }
}