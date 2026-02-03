// Abstract car pas utilisée fonctionnellement => juste une base 
abstract class Vertebre
{
    public string Race { get; set; }

    // vitual => permet aux classes qui héritent de réécrire la propriété (override)
    public virtual double Poids { get; set; }
}