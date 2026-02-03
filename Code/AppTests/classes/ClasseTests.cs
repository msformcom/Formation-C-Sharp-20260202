[TestClass]
public class ClasseTests
{
    [TestMethod]
    public void Instanciation()
    {
        var c=new Chien(){Nom="Medor", Poids=89};
        var catalogueZoo=new List<Vertebre>();
        catalogueZoo.Add(c);
        catalogueZoo.Add(new Chat());

        var vertebre1=catalogueZoo.First();
        if (vertebre1.GetType() == typeof(Chien))
        {
            //var v=((Chat)vertebre1);
        }

        Object o=new Chien();

        var chat=((Chat)o); // Erreur
        var chien=((Chien)o); // marche
        var v=(Vertebre)o; // marche
    }
}