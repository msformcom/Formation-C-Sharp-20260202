
namespace AppTests;

[TestClass] // Attribut indiquant que cette classe contient des tests unitaires
public sealed class TypesNumeriquestests
{
    [TestMethod]
    public void IntTests()
    {
        int? a;   //Int32? a;

        Int32 b = 1;

        string s = "kjhkj";

        int c = int.MaxValue;
        unchecked
        {
            c = c + 1;
        }

        a = null;

        // var => inférence de type
        int i = 1;
        double j = ((double)i) / 2D; // j != 0.5
        int j2 = (int)j;

    }

    [TestMethod]
    public void DoubleTests()
    {
        // chifres à virgule flottante
        // single = 32 bits nombre de chiffres après la virgule : 7 Approximatif
        // double = 64 bits  nombre de chiffres après la virgule : 15 Approximatif
        // decimal = 128 bits nombre de chiffres après la virgule : 28-29 precis
        double a = 0D;
        for (int i = 0; i < 10; i++)
        {
            a += 0.3D;
        }
        bool test = (a == 3D); // false


        double zero=0;

        double v=1/zero; 
        double nv = -1 / zero;
        double z=1/double.PositiveInfinity; // 0
        double zn=-0;


    }
    [TestMethod]
    public void DecimalTests()
    {
        // chifres à virgule flottante
        // single = 32 bits nombre de chiffres après la virgule : 7 Approximatif
        // double = 64 bits  nombre de chiffres après la virgule : 15 Approximatif
        // decimal = 128 bits nombre de chiffres après la virgule : 28-29 precis
        decimal a = 0M;
        int i=0;
        for ( i = 0; i < 10; i++)
        {
            int b=0;
            a += 0.3M;
        }

        Console.WriteLine(i);
        bool test = (a == 3M); // true
        decimal z=0M;
        var inf=1/z;

    }

    [TestMethod]
    public void StringTests()
    {
        // les chaines de caractères sont immuables
        // problemùme c est stocké en mémoire 1000 fois
        string c = "";
        for (int i = 0; i < 1000; i++)
        {
            c += "a";
        }

        // solution  StringBuilder
        // objet mutable qui gère ka mémoire de façon optimisée pour les concaténations
        System.Text.StringBuilder sb = new System.Text.StringBuilder("*");
        for (int i = 0; i < 1000; i++)
        {
            sb.Append("a");
        }
        var result = sb.ToString();
    }



}
