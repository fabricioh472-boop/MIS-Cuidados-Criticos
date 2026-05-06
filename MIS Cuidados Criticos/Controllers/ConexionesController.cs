using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIS_Cuidados_Criticos.Data;
using MIS_Cuidados_Criticos.Dominio;

namespace MIS_Cuidados_Criticos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConexionesController : ControllerBase
    {
        private readonly EnfermeriaService _enfermeriaService;
        private readonly ApplicationDbContext _context;

        public ConexionesController(
            EnfermeriaService enfermeriaService,
            ApplicationDbContext context)
        {
            _enfermeriaService = enfermeriaService;
            _context = context;
        }

        // =========================
        // GET ENFERMERAS
        // =========================
        [HttpGet("enfermeras-disponibles")]
        public async Task<IActionResult> EnfermerasDisponibles()
        {
            var data = await _enfermeriaService.ObtenerEnfermerasDisponibles();

            if (data == null)
                return BadRequest("No se pudo obtener datos del microservicio");

            return Ok(data);
        }

        // =========================
        // RECIBIR PACIENTE
        // =========================
        [HttpPost("recibir-paciente-logistica")]
        public async Task<IActionResult> RecibirPaciente(
            [FromQuery] string codigo,
            [FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre))
                return BadRequest("Código y nombre son obligatorios");

            var paciente = new Paciente
            {
                Estado = "Activo",
                Codigo = codigo.ToLower(),
                Nomre = nombre.ToLower()
            };

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Paciente recibido correctamente",
                paciente.Codigo,
                paciente.Nomre
            });
        }
    }
}