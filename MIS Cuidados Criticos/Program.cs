using Microsoft.EntityFrameworkCore;
using MIS_Cuidados_Criticos.Data;

var builder = WebApplication.CreateBuilder(args);

//
// 🔥 CORS
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
// 🔥 JSON FIX (EVITA ERROR 500 POR RELACIONES CIRCULARES)
//
builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

//
// 🔥 CONNECTION STRING
//
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

string connectionString;

if (!string.IsNullOrWhiteSpace(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');

    connectionString =
        $"Host={uri.Host};" +
        $"Port={uri.Port};" +
        $"Database={uri.AbsolutePath.TrimStart('/')};" +
        $"Username={userInfo[0]};" +
        $"Password={userInfo[1]};" +
        $"SSL Mode=Require;Trust Server Certificate=true";
}
else
{
    connectionString =
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Host=localhost;Port=5432;Database=MIS_Cuidados_Criticos;Username=postgres;Password=as";
}

//
// 🔥 DB CONTEXT
//
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString, npgsql =>
    {
        npgsql.EnableRetryOnFailure(3);
        npgsql.CommandTimeout(30);
    })
);

//
// 🔥 SWAGGER
//
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//
// 🔥 DEBUG ENDPOINT (IMPORTANTE PARA PROBAR RAILWAY)
//
app.MapGet("/ping", () => "OK");

//
// 🔥 MIGRACIONES (PROTEGIDAS)
//
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        // 🔴 SI SIGUE 500, comenta esto primero para probar
        db.Database.Migrate();
        Console.WriteLine("✔ Migraciones OK");
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Migraciones error: " + ex.Message);
    }
}

//
// 🔥 PIPELINE
//
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("MyApp");

app.UseAuthorization();

app.MapControllers();

//
// 🔥 ROOT
//
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();