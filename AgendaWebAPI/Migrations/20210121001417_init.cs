using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgendaWebAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: true),
                    Sobrenome = table.Column<string>(type: "longtext", nullable: true),
                    Telefone = table.Column<string>(type: "longtext", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contatos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PessoasEventos",
                columns: table => new
                {
                    ContatoId = table.Column<int>(type: "int", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoasEventos", x => new { x.ContatoId, x.EventoId });
                    table.ForeignKey(
                        name: "FK_PessoasEventos_Contatos_ContatoId",
                        column: x => x.ContatoId,
                        principalTable: "Contatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PessoasEventos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Contatos",
                columns: new[] { "Id", "Email", "Nome", "Sobrenome", "Telefone" },
                values: new object[,]
                {
                    { 1, "chapolin@email.com", "Chapolin", "Colorado", "123456789" },
                    { 2, "chris@email.com", "Chris", "Andriotti", "2134234" },
                    { 3, "tripa@email.com", "Tripa", "Seca", "345345" },
                    { 4, "luiza@email.com", "Luiza", "Gutierrez", "465456" }
                });

            migrationBuilder.InsertData(
                table: "Eventos",
                columns: new[] { "Id", "Data", "Nome" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reunião Comercial" },
                    { 2, new DateTime(1991, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aniversario" },
                    { 3, new DateTime(2021, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Curso ASP.NET Core" }
                });

            migrationBuilder.InsertData(
                table: "PessoasEventos",
                columns: new[] { "ContatoId", "EventoId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PessoasEventos_EventoId",
                table: "PessoasEventos",
                column: "EventoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PessoasEventos");

            migrationBuilder.DropTable(
                name: "Contatos");

            migrationBuilder.DropTable(
                name: "Eventos");
        }
    }
}
