using Microsoft.EntityFrameworkCore;
using MIS_Cuidados_Criticos.Dominio;

namespace MIS_Cuidados_Criticos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<SignoVital> SignosVitales { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<SignoAlerta> SignoAlertas { get; set; }
        public DbSet<AlertaPaciente> AlertaPacientes { get; set; }
        public DbSet<SignoPaciente> SignoPacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // SignoAlerta -> SignoVital
            modelBuilder.Entity<SignoAlerta>()
                .HasOne(sa => sa.SignoVital)
                .WithMany(s => s.signoAlertas)
                .HasForeignKey(sa => sa.Id_signo_vital);

            // SignoAlerta -> Alerta
            modelBuilder.Entity<SignoAlerta>()
                .HasOne(sa => sa.Alerta)
                .WithMany(a => a.SignoAlertas)
                .HasForeignKey(sa => sa.Id_alerta);

            // AlertaPaciente -> Alerta
            modelBuilder.Entity<AlertaPaciente>()
                .HasOne(ap => ap.alerta)
                .WithMany(a => a.AlertaPacientes)
                .HasForeignKey(ap => ap.Id_alerta);

            // AlertaPaciente -> Paciente
            modelBuilder.Entity<AlertaPaciente>()
                .HasOne(ap => ap.paciente)
                .WithMany(p => p.AlertaPacientes)
                .HasForeignKey(ap => ap.Id_Paciente);

            // SignoPaciente -> SignoVital
            modelBuilder.Entity<SignoPaciente>()
                .HasOne(sp => sp.signoVital)
                .WithMany(s => s.signopacientes)
                .HasForeignKey(sp => sp.id_signo);

            // SignoPaciente -> Paciente
            modelBuilder.Entity<SignoPaciente>()
                .HasOne(sp => sp.paciente)
                .WithMany(p => p.SignoPacientes)
                .HasForeignKey(sp => sp.Id_paciente);
        }
    }
}