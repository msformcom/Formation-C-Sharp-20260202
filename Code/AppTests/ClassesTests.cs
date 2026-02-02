using MaBiblio;

[TestClass]
public sealed class ClassesTests
{
  [TestMethod]

  public void PersonneAgeNegatifTest()
  {
    // Arrange => Préparer les conditions du test
    Personne p = new Personne();
    Exception? erreurSurvenue=null;
    //var p2=p; // => p et p2 sont des références vers 1 seul emplacement mémoire (du tas)

    // Act => exécuter l'action à tester
    try
    {
          p.Age = -2;
    }
    catch (System.Exception ex)
    {
      erreurSurvenue=ex;
    }


    // Assert => vérifier le reésultat => ici une erreur doit être survenue
    Assert.IsNotNull(erreurSurvenue,"Une valeur négative pour l'age n'entraine pas d'erreur");
    Assert.IsInstanceOfType<ArgumentOutOfRangeException>(erreurSurvenue,"Le type erreur n'est pas bon");



  }


}
