using System.Security.Cryptography.X509Certificates;


// Je definis un délégue => je donne un nom à une signature
public delegate int OperationSurDeuxEntiers(int a, int b);

[TestClass]
public class MethodesTests
{
    [TestMethod]
    public void FonctionsTests()
    {
        var a = Addtition(1, 2);

        // Fonction définié de manière déclarative (dans le bloc de code)
        // Connue avant que le code de ce block commence à s'exécuter
        // void si la méthode ne renvoit pas de valeur => Méthode = Soit une fonction soit un sub
        int Addtition(int nombreA, int nombreB)
        {
            return nombreA + nombreB;
        }

        int Soustraction(int a, int b) => a - b;

        // Création de variable de type fonction
        // deux int en paramètres => double
        // Func<int,int,double> => Délégué (signature de la fonction)
        // multiplication => Nom de la variable
        // (a,b)=>a*b => code de la fonction avec syntaxe dite "lambda" ou "fléchée"
        // Attention : multiplication est une variable => initialisée quand le code exécute la ligne
        Func<int, int, double> multiplication = (a, b) => a * b;
        multiplication = (a, b) =>
        {
            return a + b;
        };

        // Création d'une variable de type fonction mais sans retour
        Action<string, string> log = (message, userName) =>
        {
            Console.WriteLine($"{userName} : {message}");
        };

        log("Bonjour", "Dom");
        var m = multiplication(1, 2);


        Func<int, DateTime> addDays = (d) => DateTime.Now.AddDays(d);
        var demain = addDays(1);



        OperationSurDeuxEntiers operation;
        // operation(1,2);
        operation = (a, b) => a / b; // opération représente la dicision
        operation = (a, b) => a % b; // operation le reste de la division de a par b

        var reste=operation(5,2); // 1

    }
}