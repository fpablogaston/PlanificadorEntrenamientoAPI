using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanificadorEntrenamientoAPI.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracionRelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rutinas_Usuarios_UsuarioId",
                table: "Rutinas");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Rutinas",
                newName: "EntrenadorId");

            migrationBuilder.RenameIndex(
                name: "IX_Rutinas_UsuarioId",
                table: "Rutinas",
                newName: "IX_Rutinas_EntrenadorId");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Usuarios");

            migrationBuilder.AddColumn<int>(
                name: "Rol",
                table: "Usuarios",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntrenadorId",
                table: "Usuarios",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlumnoId",
                table: "Rutinas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EntrenadorId",
                table: "Usuarios",
                column: "EntrenadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Rutinas_AlumnoId",
                table: "Rutinas",
                column: "AlumnoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rutinas_Usuarios_AlumnoId",
                table: "Rutinas",
                column: "AlumnoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rutinas_Usuarios_EntrenadorId",
                table: "Rutinas",
                column: "EntrenadorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Usuarios_EntrenadorId",
                table: "Usuarios",
                column: "EntrenadorId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rutinas_Usuarios_AlumnoId",
                table: "Rutinas");

            migrationBuilder.DropForeignKey(
                name: "FK_Rutinas_Usuarios_EntrenadorId",
                table: "Rutinas");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Usuarios_EntrenadorId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_EntrenadorId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Rutinas_AlumnoId",
                table: "Rutinas");

            migrationBuilder.DropColumn(
                name: "EntrenadorId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "AlumnoId",
                table: "Rutinas");

            migrationBuilder.RenameColumn(
                name: "EntrenadorId",
                table: "Rutinas",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Rutinas_EntrenadorId",
                table: "Rutinas",
                newName: "IX_Rutinas_UsuarioId");

            migrationBuilder.AlterColumn<string>(
                name: "Rol",
                table: "Usuarios",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Rutinas_Usuarios_UsuarioId",
                table: "Rutinas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
