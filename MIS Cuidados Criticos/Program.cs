using Microsoft.EntityFrameworkCore;
using MIS_Cuidados_Criticos.Data;

var builder = WebApplication.CreateBuilder(args);

// Connection string local
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Railway override
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (!string.IsNullOrEmpty(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');

    connectionString =
        $"Host={uri.Host};" +
        $"Port={uri.Port};" +
        $"Database={uri.AbsolutePath.Trim('/')};" +
        $"Username={userInfo[0]};" +
        $"Password={userInfo[1]};" +
        $"SSL Mode=Require;Trust Server Certificate=true";
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddHttpClient<EnfermeriaService>();
builder.Services.AddHttpClient<LogisticaService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger SIEMPRE activo
app.UseSwagger();
app.UseSwaggerUI();

// Redirigir raíz
app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseAuthorization();
app.MapControllers();

app.Run();