using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIS_Cuidados_Criticos.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlertaPacientes_Alertas_AlertaId",
                table: "AlertaPacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_AlertaPacientes_Pacientes_PacienteId",
                table: "AlertaPacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_SignoAlertas_Alertas_AlertaId",
                table: "SignoAlertas");

            migrationBuilder.DropForeignKey(
                name: "FK_SignoAlertas_SignosVitales_SignoVitalId",
                table: "SignoAlertas");

            migrationBuilder.DropForeignKey(
                name: "FK_SignoPacientes_Pacientes_PacienteId",
                table: "SignoPacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_SignoPacientes_SignosVitales_SignoVitalId",
                table: "SignoPacientes");

            migrationBuilder.DropIndex(
                name: "IX_SignoPacientes_PacienteId",
                table: "SignoPacientes");

            migrationBuilder.DropIndex(
                name: "IX_SignoPacientes_SignoVitalId",
                table: "SignoPacientes");

            migrationBuilder.DropIndex(
                name: "IX_SignoAlertas_AlertaId",
                table: "SignoAlertas");

            migrationBuilder.DropIndex(
                name: "IX_SignoAlertas_SignoVitalId",
                table: "SignoAlertas");

            migrationBuilder.DropIndex(
                name: "IX_AlertaPacientes_AlertaId",
                table: "AlertaPacientes");

            migrationBuilder.DropIndex(
                name: "IX_AlertaPacientes_PacienteId",
                table: "AlertaPacientes");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "SignoPacientes");

            migrationBuilder.DropColumn(
                name: "SignoVitalId",
                table: "SignoPacientes");

            migrationBuilder.DropColumn(
                name: "AlertaId",
                table: "SignoAlertas");

            migrationBuilder.DropColumn(
                name: "SignoVitalId",
                table: "SignoAlertas");

            migrationBuilder.DropColumn(
                name: "AlertaId",
                table: "AlertaPacientes");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "AlertaPacientes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "SignoPacientes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SignoVitalId",
                table: "SignoPacientes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlertaId",
                table: "SignoAlertas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SignoVitalId",
                table: "SignoAlertas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlertaId",
                table: "AlertaPacientes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "AlertaPacientes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SignoPacientes_PacienteId",
                table: "SignoPacientes",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_SignoPacientes_SignoVitalId",
                table: "SignoPacientes",
                column: "SignoVitalId");

            migrationBuilder.CreateIndex(
                name: "IX_SignoAlertas_AlertaId",
                table: "SignoAlertas",
                column: "AlertaId");

            migrationBuilder.CreateIndex(
                name: "IX_SignoAlertas_SignoVitalId",
                table: "SignoAlertas",
                column: "SignoVitalId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertaPacientes_AlertaId",
                table: "AlertaPacientes",
                column: "AlertaId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertaPacientes_PacienteId",
                table: "AlertaPacientes",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlertaPacientes_Alertas_AlertaId",
                table: "AlertaPacientes",
                column: "AlertaId",
                principalTable: "Alertas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlertaPacientes_Pacientes_PacienteId",
                table: "AlertaPacientes",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SignoAlertas_Alertas_AlertaId",
                table: "SignoAlertas",
                column: "AlertaId",
                principalTable: "Alertas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SignoAlertas_SignosVitales_SignoVitalId",
                table: "SignoAlertas",
                column: "SignoVitalId",
                principalTable: "SignosVitales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SignoPacientes_Pacientes_PacienteId",
                table: "SignoPacientes",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SignoPacientes_SignosVitales_SignoVitalId",
                table: "SignoPacientes",
                column: "SignoVitalId",
                principalTable: "SignosVitales",
                principalColumn: "Id");
        }
    }
}
