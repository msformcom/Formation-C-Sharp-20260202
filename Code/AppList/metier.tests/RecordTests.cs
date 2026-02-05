using System.Data.Common;

[TestClass]
public class RecordTests
{
    [TestMethod]
    public void RecordTest()
    {
        (int Id,string Libelle) a=(1,"coucou");
        (int,string) b=(1,"Toto");
        a=b;
        a.Id=6;
        a.Id=b.Item1;
        b=a;
        
    }
}