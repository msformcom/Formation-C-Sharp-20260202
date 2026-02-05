[TestClass]
public class ListeTests
{

    [TestMethod]
    public void TP_Liste()
    {
        var entiers=Enumerable.Range(1,10000);
        // Créer une liste à partir de entiers avec des nombres aléatoire
        var r=new Random();
        // r.Next(0,200)  genére un nombre aléatoire
        var entiersAleatoires=entiers.Select(c=>r.Next(0,200)).Distinct().ToList();
        // sur entiersEleatoires => proportion dont le cosinus <0.1
        var prop=entiersAleatoires.Select(c=>Math.Cos(c)).Where(c=>c<0.1).Count()/entiersAleatoires.Count();
    }

    [TestMethod]
    public void IEnumerable()
    {
        
        var entiers =new List<int>(){1,7,3,2,3,5,9,2,5};
        var petitsEniers=entiers // 1,7,3,2,3,5,9,2,5
                .Skip(2)// 3,2,3,5,9,2,5
                .Take(10) // 3,2,3,5,9,2,5
                .Where(c=>
            c<6
            )   // 3,2,3,5,2,5
            
             .ToList() //  3,2,3,5,2,5
             
             .Take(2) //3,2   .ToArray() matérialise les éléments sélectionnés en mémoire
            ;
        var count=petitsEniers.Count(); 
        entiers.Add(4);
        count=petitsEniers.Count();
        count=petitsEniers.Count();

        string s="toto"; //       t    o     t    o
        foreach(var e in s)
        {
            
        }

        // GetEnumerator est le membre obligatoire de IEnumerable
        var enumerateurDesElementsDeS=s.GetEnumerator();
        while (enumerateurDesElementsDeS.MoveNext())
        {
            var e=enumerateurDesElementsDeS.Current;
        }





    }

    [TestMethod]
    public void MyTestMethod()
    {
        // Generator
        IEnumerable<int> GetAllEntiers()
        {
           var i=0;
            while (i<=int.MaxValue)
            {
                yield return i;
                i++;
            }
        }

        var l=GetAllEntiers();
        var enumerateur=l.GetEnumerator();

        // afficher 10
        void AfficherElements(int n)
        {
            for(var i = 0; i < n; i++)
            {
                enumerateur.MoveNext();
                Console.WriteLine(enumerateur.Current);
            }           
        }

        AfficherElements(2);
        AfficherElements(2);




    }



    [TestMethod]
    public void StringEnumerable()
    {
        var texte="Le chien a mordu le facteur";
        var sansEspacces=texte.Where(c=>c!=' ');
       var  groupes= sansEspacces.GroupBy(c=>c);
       var groupesOrdones=groupes.OrderBy(g=>g.Key);
       var resultat=groupesOrdones.Select(g=>new{ lettre=g.Key,nombre =g.Count()});
       var top =resultat.OrderByDescending(c=>c.nombre).First();
    }



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