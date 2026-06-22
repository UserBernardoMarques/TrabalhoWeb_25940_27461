using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabalhoWeb_25940.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipanteWorkshop_Participantes_ParticipantesId",
                table: "ParticipanteWorkshop");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipanteWorkshop_Workshops_WorkshopsId",
                table: "ParticipanteWorkshop");

            migrationBuilder.DropForeignKey(
                name: "FK_Workshops_Categorias_CategoriaId",
                table: "Workshops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParticipanteWorkshop",
                table: "ParticipanteWorkshop");

            migrationBuilder.RenameTable(
                name: "ParticipanteWorkshop",
                newName: "WorkshopParticipantes");

            migrationBuilder.RenameIndex(
                name: "IX_ParticipanteWorkshop_WorkshopsId",
                table: "WorkshopParticipantes",
                newName: "IX_WorkshopParticipantes_WorkshopsId");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Workshops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Workshops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Aprovado",
                table: "Workshops",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ImagemPath",
                table: "Workshops",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Participantes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Participantes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkshopParticipantes",
                table: "WorkshopParticipantes",
                columns: new[] { "ParticipantesId", "WorkshopsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkshopParticipantes_Participantes_ParticipantesId",
                table: "WorkshopParticipantes",
                column: "ParticipantesId",
                principalTable: "Participantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkshopParticipantes_Workshops_WorkshopsId",
                table: "WorkshopParticipantes",
                column: "WorkshopsId",
                principalTable: "Workshops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workshops_Categorias_CategoriaId",
                table: "Workshops",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkshopParticipantes_Participantes_ParticipantesId",
                table: "WorkshopParticipantes");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkshopParticipantes_Workshops_WorkshopsId",
                table: "WorkshopParticipantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Workshops_Categorias_CategoriaId",
                table: "Workshops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkshopParticipantes",
                table: "WorkshopParticipantes");

            migrationBuilder.DropColumn(
                name: "Aprovado",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "ImagemPath",
                table: "Workshops");

            migrationBuilder.RenameTable(
                name: "WorkshopParticipantes",
                newName: "ParticipanteWorkshop");

            migrationBuilder.RenameIndex(
                name: "IX_WorkshopParticipantes_WorkshopsId",
                table: "ParticipanteWorkshop",
                newName: "IX_ParticipanteWorkshop_WorkshopsId");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Workshops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Workshops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Participantes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Participantes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParticipanteWorkshop",
                table: "ParticipanteWorkshop",
                columns: new[] { "ParticipantesId", "WorkshopsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipanteWorkshop_Participantes_ParticipantesId",
                table: "ParticipanteWorkshop",
                column: "ParticipantesId",
                principalTable: "Participantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipanteWorkshop_Workshops_WorkshopsId",
                table: "ParticipanteWorkshop",
                column: "WorkshopsId",
                principalTable: "Workshops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workshops_Categorias_CategoriaId",
                table: "Workshops",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
