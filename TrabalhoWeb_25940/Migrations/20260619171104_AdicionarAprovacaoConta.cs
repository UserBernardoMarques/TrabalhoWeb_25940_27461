using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabalhoWeb_25940.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarAprovacaoConta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Aprovado",
                table: "Participantes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aprovado",
                table: "Participantes");
        }
    }
}
