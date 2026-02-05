using System.Net.Mime;

using Metier;
var liste = new Liste("Ma liste");
liste.AddElement(new ElementListe("Pates", 6));
liste.AddElement(new ElementListe("Riz", 1));

// Builder : Sert à préparer notre application
var builder = WebApplication.CreateBuilder(args);

// builder.Services => Configuration de l'injection de dépendance
// Cors => Service qui ajoute des headers pour autoriser
// Certains domaines (localhost:4200) à utiliser les données renvoyées
builder.Services.AddCors(o =>
    o.AddPolicy("dev", p => p
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
        )
    );

// Création d'une application web configurée
var app = builder.Build();
app.UseCors("dev");

app.UseHttpsRedirection();

// Je mets en place mes middleware de journalisation
app.Use(async (HttpContext context, Func<Task> next) =>
{
    // Traitement avant de passer la main
    Console.WriteLine("Entree  {0} : {1}", DateTime.Now, context.Request.Path);
    // Passer la main au suivant
    await next();
    // Traitement après les traitements réalisés par les middleware qui suivent
    Console.WriteLine("Fin {0} : {1}", DateTime.Now, context.Request.Path);

});

// redirige / vers index.html
app.UseDefaultFiles();

// Ajoute un middleware qui si l'url correspond à un fichier de wwwroot renvoit le fichier
app.UseStaticFiles();



// GET : /liste renvoit en json la liste définie en haut
app.MapGet("/liste", async (context) =>
{

    await context.Response.WriteAsJsonAsync(liste);
});


// On lance le server
app.Run();
