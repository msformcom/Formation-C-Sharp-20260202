// Injecteur de dépendance
// Attention ! Les classes de Dependencyinjection ne sont pas fournies par défaut
// dotnet add package "Microsoft.Extensions.DependencyInjection"
using System.Reflection;
using Metier;
using Microsoft.EntityFrameworkCore;
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
        configBuilder.AddJsonFile("appsettings.json");

        var config = configBuilder.Build();
        var test = config.GetSection("ListeFolder").Value;
        services.AddSingleton<IConfiguration>(config);
        #endregion

        #region Journalisation
        // Création du systeme de journalisation
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            // builder.AddConsole();
            builder.AddDebug();
        });
        services.AddSingleton<ILoggerFactory>(loggerFactory);
        services.AddLogging();
        #endregion

        #region ClassesDePersistence
        // Ajout d'une dépendance associée à IPersist<Guid,Liste,string>
        // qui est dirigée vers PersistListeToDisk en mod singleton
        var chaineClassPersistence = config.GetSection("configuration:PersistanceClass").Value;
        // Chargement de l'assembly dans laquelle se trouve la classe de persistence
        var assemblyPersiste = Assembly.Load("persist");
        // Chercher le type de la classe dans ml'assembly
        var typeClassPersistence = assemblyPersiste.GetType(chaineClassPersistence);

        services.AddSingleton(typeof(IPersist<Guid, Liste, string>), typeClassPersistence);
        #endregion

        #region Config de la BDD
        services.AddDbContext<ListeContext>(builder =>
        {
            // Le package Microsoft.EntityFrameworkCore.SqlServer
            // fournit une fonction pour configurer le DbContext
            // Avec le provider de SQL Server
            // Cette fonction ajoute un objet DbContextOptions à l'injection de dépendance
            builder.UseSqlServer("name=ListeDbConnection");
            // Grace au design patter builder
            // je vais configurer la BDD grace à l'objet builder conçu pour
        });
        #endregion

        #region Mappage Poco <=> DAO
        // Ajout de automapper à l'injection de dépendance
        services.AddAutoMapper(options =>
        {
            // Création des mappings entre les Liste vers ListeDAO
            options.CreateMap<Liste, ListeDAO>()
            // Elements de ListeDAO doit être associée à MyElements dans la Liste
            .ForMember(c => c.Elements, o =>
            {
                o.MapFrom(c => c.MyElements);
            })
            // Ajouter le mapprin de ListeDAO vers Liste
            .ReverseMap()
                .ForMember(c => c.Elements, o => o.Ignore())
                .ForMember(c => c.MyElements, o => o.MapFrom(c => c.Elements))
            ;
            options.CreateMap<ElementListe, ElementListeDAO>()
                // .ForMember(c => c.Achete, o => o.MapFrom(c => c._Achete))
                // .ForMember(c => c.Libele, o => o.MapFrom(c => c._Libele))
                //  .ForMember(c => c.Nombre, o => o.MapFrom(c => c._Nombre))
            .ReverseMap();
        });

        #endregion

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