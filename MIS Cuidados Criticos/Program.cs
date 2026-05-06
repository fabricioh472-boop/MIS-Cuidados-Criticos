using Microsoft.EntityFrameworkCore;
using MIS_Cuidados_Criticos.Data;

var builder = WebApplication.CreateBuilder(args);

//
// CORS
//
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyApp", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//
// JSON FIX (EVITA CICLOS)
//
builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

//
// CONNECTION STRING (RAILWAY + LOCAL)
//
string connectionString;

var host = Environment.GetEnvironmentVariable("PGHOST");
var port = Environment.GetEnvironmentVariable("PGPORT");
var database = Environment.GetEnvironmentVariable("PGDATABASE");
var user = Environment.GetEnvironmentVariable("PGUSER");
var password = Environment.GetEnvironmentVariable("PGPASSWORD");

if (!string.IsNullOrWhiteSpace(host))
{
    if (string.IsNullOrWhiteSpace(port) ||
        string.IsNullOrWhiteSpace(database) ||
        string.IsNullOrWhiteSpace(user) ||
        string.IsNullOrWhiteSpace(password))
    {
        throw new Exception("Faltan variables de entorno PG en Railway");
    }

    connectionString =
        $"Host={host};" +
        $"Port={port};" +
        $"Database={database};" +
        $"Username={user};" +
        $"Password={password};" +
        $"SSL Mode=Require;Trust Server Certificate=true";
}
else
{
    connectionString =
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Host=localhost;Port=5432;Database=MIS_Cuidados_Criticos;Username=postgres;Password=as";
}

//
// DB CONTEXT
//
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString, npgsql =>
    {
        npgsql.EnableRetryOnFailure(3);
        npgsql.CommandTimeout(60);
    })
);

//
// SWAGGER
//
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//
// TEST ENDPOINT
//
app.MapGet("/ping", () => "OK");

//
// MIGRACIONES (DESACTIVADAS EN RAILWAY PARA EVITAR CRASH)
//
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        // IMPORTANTE: en Railway esto puede romper el deploy
        // db.Database.Migrate();

        Console.WriteLine("Migraciones desactivadas en runtime (usar manual o CI)");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error migraciones: " + ex.Message);
    }
}

//
// PIPELINE
//
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("MyApp");

app.UseAuthorization();

app.MapControllers();

//
// ROOT
//
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();