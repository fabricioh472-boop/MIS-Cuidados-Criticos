using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MIS_Cuidados_Criticos.Data;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// HTTP CLIENT para consumir otros sistemas
builder.Services.AddHttpClient();

// CORS (IMPORTANTE para conectar frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- RAILWAY ---
// Se habilitan Swagger y SwaggerUI fuera del bloque IsDevelopment
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Frontend")),
    RequestPath = ""
});
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Proyecto_H API V1");
    c.RoutePrefix = string.Empty; // Esto hace que Swagger cargue en la raíz del link
});
// --------------------------------

// 🔴 ACTIVAR CORS (ANTES de MapControllers)
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();