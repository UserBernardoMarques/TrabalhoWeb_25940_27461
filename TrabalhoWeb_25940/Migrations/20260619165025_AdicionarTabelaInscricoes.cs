using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabalhoWeb_25940.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarTabelaInscricoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ImagemPath",
                table: "Workshops");

            migrationBuilder.RenameTable(
                name: "WorkshopParticipantes",
                newName: "ParticipanteWorkshop");

            migrationBuilder.RenameIndex(
                name: "IX_WorkshopParticipantes_WorkshopsId",
                table: "ParticipanteWorkshop",
                newName: "IX_ParticipanteWorkshop_WorkshopsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParticipanteWorkshop",
                table: "ParticipanteWorkshop",
                columns: new[] { "ParticipantesId", "WorkshopsId" });

            migrationBuilder.CreateTable(
                name: "Inscricoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipanteId = table.Column<int>(type: "int", nullable: false),
                    WorkshopId = table.Column<int>(type: "int", nullable: false),
                    DataPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Aprovada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscricoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscricoes_Participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscricoes_Workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inscricoes_ParticipanteId",
                table: "Inscricoes",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscricoes_WorkshopId",
                table: "Inscricoes",
                column: "WorkshopId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Inscricoes");

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

            migrationBuilder.AddColumn<string>(
                name: "ImagemPath",
                table: "Workshops",
                type: "nvarchar(max)",
                nullable: true);

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
    }
}
