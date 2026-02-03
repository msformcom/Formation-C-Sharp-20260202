[TestClass]
public class ListeTests
{
    [TestMethod]
    public void Liste()
    {
        // List = ensemble d'éléments
        // classe générique => List d'un type de données
        // entiers est une liste de int
        List<int>  entiers=new List<int>(){1,2,3}; // 1,2,3
        entiers.Add(2); // 1,2,3,2
        entiers.Add(4); // 1,2,3,2,4
        entiers.Remove(2); // 1,3,2,4
        entiers.RemoveAt(2); // 1,3,4 => indexation en 0
        entiers.IndexOf(3); // 1

        // parcourir les éléments
        for(var i = 0; i < entiers.Count; i++)
        {
            var e=entiers[i];
            if (e % 2 == 0)
            {
                entiers.Remove(e);
            }
        }

        // enlever les entiers pairs
        foreach(var e in entiers)
        {
            if (e % 2 == 0)
            {
                // Liste non modifiable pendant l'itération avec foreach
                //entiers.Remove(e);
            }
        }

        entiers=new() {1,2,3,4,5,6,7,8,9,10,11,12};
        // Linq => ensemble de fonctions qui travaillent sur des collections
        
        var selection=entiers.Where(c=>c>3) //4,5,6,7,8,9,10,11,12
                    .Skip(2) // 6,7,8,9,10,11,12
                    .Take(5) // 6,7,8,9,10
                    .Reverse() // 10,9,8,7,6
                    .Select(c=>c*2) // 20,18,16,14,12
                    .OrderBy(c=>c%3) //18,12,16,20,14
                    .ToList();
        var moyenne=selection.Sum(c=>Math.Cos(c));


        entiers.RemoveAll(c=>
            c%2==0
            );

        var doubles=    entiers.Select(c=>
        c.ToString()
        ).ToList();

        var grosEntiers=entiers.Where(c=>c>3).ToList();
        


        // Cette fonction est 
        Func<int,bool> filtre=(int a)=> a%2==0;

        void EnleveValeur(List<int> liste,Func<int, bool> filtre )
        {
            for(var i = liste.Count-1; i >=0; i--)
            {
                var e=liste[i];
                if (filtre(e))
                {
                    liste.RemoveAt(i);
                }
            }            
        }

        EnleveValeur(entiers,filtre);


    }
}