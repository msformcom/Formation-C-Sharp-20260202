sealed class Chiwawa : Chien
{
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