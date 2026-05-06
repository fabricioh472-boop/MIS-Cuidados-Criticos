using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIS_Cuidados_Criticos.Data;
using MIS_Cuidados_Criticos.Dominio;
using MIS_Cuidados_Criticos.DTOs;

namespace MIS_Cuidados_Criticos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignoVitalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SignoVitalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTAR
        [HttpGet]
        public async Task<IActionResult> ConseguiSignos()
        {
            var dato = await _context.SignosVitales
                .Where(a => a.Estado == "Activo")
                .Select(a => new
                {
                    a.Codigo,
                    a.Frecuencia_cardiaca,
                    a.Presion_arterial,
                    a.Saturacion_oxigeno
                })
                .ToListAsync();

            return Ok(dato);
        }

        // BUSCAR
        [HttpGet("{codigo}")]
        public async Task<IActionResult> ConseguiSignosporCod(string codigo)
        {
            var dato = await _context.SignosVitales
                .Where(a => a.Codigo == codigo && a.Estado == "Activo")
                .Select(a => new
                {
                    a.Codigo,
                    a.Frecuencia_cardiaca,
                    a.Presion_arterial,
                    a.Saturacion_oxigeno
                })
                .FirstOrDefaultAsync();

            if (dato == null) return NotFound();
            return Ok(dato);
        }

        // CREAR
        [HttpPost]
        public async Task<IActionResult> AgregarSignos([FromBody] SignoVitalDTO dto)
        {
            if (dto == null)
                return BadRequest("DTO vacío");

            var dato = new SignoVital
            {
                Estado = "Activo",
                Codigo = dto.codigo.ToLower(),
                Frecuencia_cardiaca = dto.frecuencia_cardiaca,
                Presion_arterial = dto.presion_arterial,
                Saturacion_oxigeno = dto.saturacion_oxigeno
            };

            _context.SignosVitales.Add(dato);
            await _context.SaveChangesAsync();

            return Ok(dato);
        }

        // UPDATE
        [HttpPut("{codigo}")]
        public async Task<IActionResult> AgregarSignosporCod(string codigo, int frecuencia_cardiaca, float saturacion_oxigeno, string presion_arterial)
        {
            var dato = await _context.SignosVitales
                .FirstOrDefaultAsync(a => a.Codigo == codigo);

            if (dato == null) return NotFound();

            dato.Frecuencia_cardiaca = frecuencia_cardiaca;
            dato.Presion_arterial = presion_arterial;
            dato.Saturacion_oxigeno = saturacion_oxigeno;

            await _context.SaveChangesAsync();

            return Ok($"El signo vital {codigo} fue actualizado");
        }

        // DELETE
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> EliminarSignos(string codigo)
        {
            var dato = await _context.SignosVitales
                .FirstOrDefaultAsync(a => a.Codigo == codigo);

            if (dato == null) return NotFound();

            dato.Estado = "Inactivo";

            await _context.SaveChangesAsync();

            return Ok("Eliminado correctamente");
        }

        // DASHBOARD RESUMIDO
        [HttpGet("CC-resumido")]
        public async Task<IActionResult> ObtenerTodo()
        {
            var dato = await (
                from a in _context.SignosVitales
                join b in _context.SignoAlertas on a.Id equals b.Id_signo_vital
                join c in _context.Alertas on b.Id_alerta equals c.Id
                where a.Estado == "Activo" && c.Estado == "Activo"
                select new
                {
                    CodigoSigno = a.Codigo,
                    FrecuenciaCardiaca = a.Frecuencia_cardiaca,
                    Presion = a.Presion_arterial,
                    Saturacion =
                        a.Saturacion_oxigeno < 85 ? "Critico" :
                        a.Saturacion_oxigeno > 92 ? "Riesgo" : "Estable",
                    CodigoAlerta = c.Codigo,
                    TipoAle = c.Tipo,
                    Nivel = c.Nivel_criticidad
                }
            ).ToListAsync();

            return Ok(dato);
        }
    }
}