using Microsoft.EntityFrameworkCore;
using MIS_Cuidados_Criticos.Data;

var builder = WebApplication.CreateBuilder(args);

//
// 🔥 1. CORS
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
// 🔥 2. CONNECTION STRING (LOCAL + RAILWAY SEGURO)
//
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

string connectionString;

if (!string.IsNullOrWhiteSpace(databaseUrl))
{
    try
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
    catch (Exception ex)
    {
        Console.WriteLine("Error parsing DATABASE_URL: " + ex.Message);
        throw;
    }
}
else
{
    connectionString =
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Host=localhost;Port=5432;Database=MIS_Cuidados_Criticos;Username=postgres;Password=as";
}

//
// 🔥 3. DB CONTEXT (MEJORADO)
//
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString, npgsql =>
    {
        npgsql.EnableRetryOnFailure(3);
        npgsql.CommandTimeout(30);
    })
);

//
// 🔥 4. CONTROLLERS + SWAGGER
//
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//
// 🔥 5. MIGRACIONES (NO ROMPER EL ARRANQUE)
//
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        db.Database.SetCommandTimeout(30);
        db.Database.Migrate();
        Console.WriteLine("✔ Migraciones OK");
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Error migraciones: " + ex.Message);
    }
}

//
// 🔥 6. SWAGGER SIEMPRE ACTIVO
//
app.UseSwagger();
app.UseSwaggerUI();

//
// 🔥 7. PIPELINE
//
app.UseCors("MyApp");

// ⚠️ Railway a veces rompe HTTPS redirection
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//
// 🔥 8. ROOT
//
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();