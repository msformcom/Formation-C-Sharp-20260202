using MaBiblio;

[TestClass]
public sealed class Employetests
{
    [TestMethod]
    public void AgeTest()
    {
        // Arrange
        Employe e=new Employe("01/01/1970");

        // Act
        int age=e.Age;

        // Assert 
        Assert.AreEqual(age,56);
    }


}
