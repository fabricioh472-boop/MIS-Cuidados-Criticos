using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIS_Cuidados_Criticos.Data;
using MIS_Cuidados_Criticos.Dominio;
using MIS_Cuidados_Criticos.DTOs;
namespace MIS_Cuidados_Criticos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertaPacienteController:ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlertaPacienteController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Alerta por paciente
        [HttpGet]
        public async Task<IActionResult> ConseguirAlertasporPaciente()
        {
            var dato = await (
                from a in _context.AlertaPacientes
                join b in _context.Alertas on a.Id_alerta equals b.Id
                join c in _context.Pacientes on a.Id_Paciente equals c.Id
                where b.Estado == "Activo" && c.Estado == "Activo"
                select new
                {
                    Paciente = c.Codigo,
                    Alerta = b.Codigo,
                    TipoAlerta = b.Tipo,
                    Nivel = b.Nivel_criticidad
                }
            ).ToListAsync();

            return Ok(dato);
        }
        //Asociar alerta a paciente
        [HttpPost]
        public async Task<IActionResult> AñadirAlertaaPaciente([FromBody] AlertaPacienteCreateDto dto)
        {
            var alerta = await _context.Alertas
                .FirstOrDefaultAsync(a => a.Codigo == dto.CodigoAlerta && a.Estado == "Activo");

            if (alerta == null)
                return BadRequest($"La alerta {dto.CodigoAlerta} no existe o está inactiva");

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.Codigo == dto.CodigoPaciente && p.Estado == "Activo");

            if (paciente == null)
                return BadRequest($"El paciente {dto.CodigoPaciente} no existe o está inactivo");

            var rela = new AlertaPaciente
            {
                Id_alerta = alerta.Id,
                Id_Paciente = paciente.Id,
            };

            _context.AlertaPacientes.Add(rela);
            await _context.SaveChangesAsync();

            return Ok("Relación creada con éxito");
        }
    }
}
