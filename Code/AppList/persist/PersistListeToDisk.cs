
using Metier;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using persist;
using System.Runtime.Serialization.Json;
using System.Text;


public class PersistListeToDisk : IPersist<Guid, Liste, string>
{
    private readonly IConfiguration config;
    private readonly ILogger<PersistListeToDisk> logger;

        // Serialier la liste => json, xml, binary
    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Liste));

    // IConfiguration sera fourni par l'injection de dépendance
    // Au moment ou il construira le PersistListeToDisk
    // ILogger sera fourni par l'injection de dépendance
    // Au moment ou il construira le PersistListeToDisk
    public PersistListeToDisk(IConfiguration config, ILogger<PersistListeToDisk> logger)
    {
        this.config = config;
        this.logger = logger;
    }
    public Task<Liste> AddAsync(Liste o, Guid id)
    {



        // Ouvrir un fichier => emplacement du fichier enregistré sous une clé ListeFolder dans la config
        string pathToFolder = config.GetSection("ListeFolder").Value;

        // Tester l'existence du dossier
        if (!Directory.Exists(pathToFolder))
        {
            Directory.CreateDirectory(pathToFolder);
        }

        string pathToFile = Path.Combine(pathToFolder, id.ToString());

        // En créant le fichier, j'obtiens un stream
        // qui permet d'écrire des données => fichier

        // FileStream est IDisposable
        // IDisposable => Dispose => Se charge de fermer toutes les resources que l'objet a pris dans le system
        using (var fileStream = File.Create(pathToFile))
        {
            // Le using garantit que Dispose sera appelée quelle que soit la manière dont on sort de ce bloc
            // => sortie normale
            // Exception
            // return 

            // Pour écrire une chaine dans le fichier on utilise un StreamWriter

            // var sw=new StreamWriter(fileStream, Encoding.Unicode);
            // sw.WriteLine("coucou");
            // sw.WriteLineAsync("coucou2");


            // Ler serializer va sérializer l'objet et écrire le résultat dans le stream => fichier
            try
            {
                serializer.WriteObject(fileStream, o);
            }
            catch (System.Exception ex)
            {
                logger.LogError("La liste n'a pas pu être enregistrée");
                throw new Exception("Impossible");
            }


            // ne pas oublier de ferler le fichier
            //fileStream.Close();
        }
        // Retour de la tache terminée avec o comme résultat
        return Task.FromResult(o);
    }

    public async Task<Liste> GetAsync(Guid id)
    {
                // Ouvrir un fichier => emplacement du fichier enregistré sous une clé ListeFolder dans la config
        string pathToFolder = config.GetSection("ListeFolder").Value;
        string pathToFile = Path.Combine(pathToFolder, id.ToString());

        //  if(File.Exists(pathToFile))
        var fi=new FileInfo(pathToFile); // DirectoryInfo
        if (!fi.Exists)
        {
            throw new FileNotFoundException();
        }
         // Le using va appeler Dispose à la sortie du bloc
         // et libérer le fichier
        using(var fileStream = fi.OpenRead())
        {
            // Afin d'exécuter la déserialisation dans un thread séparé
            return await Task.Run(() =>
            {
                var liste=(Liste)serializer.ReadObject(fileStream);
                if (liste == null)
                {
                    throw new NullReferenceException();
                }
                logger.LogInformation($"{id} restaurée avec succès");
                return liste;               
            });


        }

    }

    public Task RemoveAsync(Guid id)
    {
        string pathToFolder = config.GetSection("ListeFolder").Value;
        string pathToFile = Path.Combine(pathToFolder, id.ToString());
        File.Delete(pathToFile);
        // var fi=new FileInfo(pathToFile);
        // fi.Delete();

        return Task.CompletedTask;
    }

    public Task<IEnumerable<(Guid Id, string search)>> SearchAsync(string texte)
    {
        throw new NotImplementedException();
    }

    public Task<Liste> UpdateAsync(Guid id, Liste o)
    {
        throw new NotImplementedException();
    }
}