using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabalhoWeb_25940.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCamposRegisto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Participantes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAluno",
                table: "Participantes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFormador",
                table: "Participantes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Participantes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Participantes");

            migrationBuilder.DropColumn(
                name: "IsAluno",
                table: "Participantes");

            migrationBuilder.DropColumn(
                name: "IsFormador",
                table: "Participantes");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Participantes");
        }
    }
}
