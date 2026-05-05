using Microsoft.EntityFrameworkCore;
using MIS_Cuidados_Criticos.Data;

var url = Environment.GetEnvironmentVariable("DATABASE");
Console.WriteLine($"Coneccion esta {url}");
var builder = WebApplication.CreateBuilder(args);
// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.WebHost.UseUrls("http:/0.0.0.0:0000");
builder.Services.AddCors(options =>
{
    options.AddPolicy("CuidadosCriticos",
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

//Constructores de servicios
builder.Services.AddHttpClient<EnfermeriaService>();
builder.Services.AddHttpClient<LogisticaService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();