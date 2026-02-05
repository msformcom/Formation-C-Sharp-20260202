//sealed sur class => ermpeche l'héritage
sealed class Chiwawa : Chien
{
    //sealed sur membre de classe  => ermpeche override
     public sealed  override double Poids
    {
        get => base.Poids;
        set
        {
            if (value > 10)
            {
                throw new Exception("C'est un blaireau");
            }
            base.Poids = value;
        }
    }
}