// Injecteur de dépendance
// Attention ! Les classes de Dependencyinjection ne sont pas fournies par défaut
// dotnet add package "Microsoft.Extensions.DependencyInjection"
using System.Reflection;
using Metier;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using persist;




[TestClass]
public class PersistTests
{
    IServiceProvider DI;
    public PersistTests()
    {
        var services = new ServiceCollection();
        #region Config
        // ConfigBuilder permet de créer un ConfigManager qui 
        // Cherchera les infos de config dans un fichier xml
        var configBuilder = new ConfigurationBuilder();
        //      <None Include=".\App.config"  /> dans le csproj
        configBuilder.AddXmlFile("App.config");
        var config = configBuilder.Build();
        var test = config.GetSection("ListeFolder").Value;
        services.AddSingleton<IConfiguration>(config);
        #endregion

        #region Journalisation
        // Création du systeme de journalisation
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
        services.AddSingleton<ILoggerFactory>(loggerFactory);
        services.AddLogging();
        #endregion

        #region ClassesDePersistence
        // Ajout d'une dépendance associée à IPersist<Guid,Liste,string>
        // qui est dirigée vers PersistListeToDisk en mod singleton
        var chaineClassPersistence = config.GetSection("PersistanceClass").Value;
        // Chargement de l'assembly dans laquelle se trouve la classe de persistence
        var assemblyPersiste = Assembly.Load("persist");
        // Chercher le type de la classe dans ml'assembly
        var typeClassPersistence = assemblyPersiste.GetType(chaineClassPersistence);

        services.AddSingleton(typeof(IPersist<Guid, Liste, string>), typeClassPersistence);
        #endregion

#region Config de la BDD
    services.AddDbContext<ListeContext>();
#region

        // Création de l'objet Injecteur de Dépendance
        DI = services.BuildServiceProvider();


    }


    [TestMethod]
    public async Task PersistTest()
    {
        var persist = DI.GetService<IPersist<Guid, Liste, string>>();
        var l = new Liste("Toto");
        l.AddElement(new ElementListe("Pate", 10));

        var id = Guid.NewGuid();
        var r = await persist.AddAsync(l, id);

        var listeRestauree = await persist.GetAsync(id);

        Assert.AreEqual(l.Libele, listeRestauree.Libele);
        Assert.AreEqual(l.Elements.Count(), listeRestauree.Elements.Count());

        await persist.RemoveAsync(id);


    }
}