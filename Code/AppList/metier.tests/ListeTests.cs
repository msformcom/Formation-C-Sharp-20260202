using Metier;

[TestClass]
public class ListeTests
{
    [TestMethod]
    public void RetourElementsTest()
    {
        // Arrange : Créer une liste, Ajouter un élément
        var liste=new Liste("Ma Liste");
        liste.AddElement(new ElementListe("Pate"));

        // Act : Lire la liste des éléments
        var elements=liste.Elements;
        var typeDeElements=elements.GetType();

        // Assert : Vérifier le type de la liste reçu (type de l'objet reçu)
        Assert.AreEqual(typeDeElements,typeof(IEnumerable<ElementListe>));
        
    }
}