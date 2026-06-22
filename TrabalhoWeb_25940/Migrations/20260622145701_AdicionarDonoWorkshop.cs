using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabalhoWeb_25940.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarDonoWorkshop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormadorId",
                table: "Workshops",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormadorId",
                table: "Workshops");
        }
    }
}
