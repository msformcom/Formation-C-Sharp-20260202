using System.Threading.Tasks;

[TestClass]
public class TaskTests
{

     Task<int> AdditionAsync(int a, int b)
    {
        // t est un objet qui représente une opération asynchrone
        var t = new Task<int>(() =>
        {
            var s = 0;
            var t = new Task(() =>
            {

                for (var i = 0; i < a; i++) s++;
                for (var i = 0; i < b; i++) s++;
            });
            return s;
        });
        // que faire avec t
        t.Start(); //  Demarrer la tache
        var terminee = t.IsCompleted; // est-elle terminee
        // Déterminer ce qu'il se passe après
        t.ContinueWith(r =>
        {
            Console.WriteLine("Resultat calculé : " + r.Result);
        });
        return t;



    }

    [TestMethod]
    public async Task TaskTest()
    {
        // fonction asynchrone => fonction qui prends du temps

        // operation asynchrone => Task
        var a = 1000000000;
        var b = 1000000000;
        // var task=AdditionAsync(a,b);
        // task.ContinueWith(r =>
        // {
        //     Console.WriteLine("Resultat calculé : " + r.Result);
        // });
        var r=await AdditionAsync(a,b);
         Console.WriteLine("Resultat calculé : " + r);
    }
}