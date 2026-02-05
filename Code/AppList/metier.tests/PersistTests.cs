// Injecteur de dépendance
// Attention ! Les classes de Dependencyinjection ne sont pas fournies par défaut
// dotnet add package "Microsoft.Extensions.DependencyInjection"
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

        // ConfigBuilder permet de créer un ConfigManager qui 
        // Cherchera les infos de config dans un fichier xml
        var configBuilder = new ConfigurationBuilder();
        //      <None Include=".\App.config"  /> dans le csproj
        configBuilder.AddXmlFile("App.config");
        var config = configBuilder.Build();
        var test = config.GetSection("ListeFolder").Value;

        // Création du systeme de journalisation
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });



        // Ajout d'une dépendance associée à IPersist<Guid,Liste,string>
        // qui est dirigée vers PersistListeToDisk en mod singleton
        services.AddSingleton<IPersist<Guid, Liste, string>, PersistListeToDisk>();
        services.AddSingleton<IConfiguration>(config);
        services.AddSingleton<ILoggerFactory>(loggerFactory);
        services.AddLogging();
        // Création de l'objet Injecteur de Dépendance
        DI = services.BuildServiceProvider();


    }


    [TestMethod]
    public async Task PersistTest()
    {
        var persist = DI.GetService<IPersist<Guid, Liste, string>>();
        var l=new Liste("Toto");
        l.AddElement(new ElementListe("Pate",10));
        var r=await  persist.AddAsync(l, Guid.NewGuid());
    }
}