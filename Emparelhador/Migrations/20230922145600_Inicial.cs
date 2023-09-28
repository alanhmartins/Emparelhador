using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emparelhador.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "torneios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rodadas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_torneios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mesas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rodada = table.Column<int>(type: "int", nullable: false),
                    torneioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mesas", x => x.id);
                    table.ForeignKey(
                        name: "FK_mesas_torneios_torneioID",
                        column: x => x.torneioID,
                        principalTable: "torneios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "jogadores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    vitorias = table.Column<int>(type: "int", nullable: false),
                    JogadorTorneioid = table.Column<int>(type: "int", nullable: true),
                    Torneioid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jogadores", x => x.id);
                    table.ForeignKey(
                        name: "FK_jogadores_torneios_Torneioid",
                        column: x => x.Torneioid,
                        principalTable: "torneios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "jogadoresTorneios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jogadorId = table.Column<int>(type: "int", nullable: false),
                    pontos = table.Column<int>(type: "int", nullable: false),
                    vitorias = table.Column<int>(type: "int", nullable: false),
                    somaAdversarios = table.Column<int>(type: "int", nullable: false),
                    torneioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jogadoresTorneios", x => x.id);
                    table.ForeignKey(
                        name: "FK_jogadoresTorneios_jogadores_jogadorId",
                        column: x => x.jogadorId,
                        principalTable: "jogadores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_jogadoresTorneios_torneios_torneioId",
                        column: x => x.torneioId,
                        principalTable: "torneios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pontosJogadoresMesa",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jogadorid = table.Column<int>(type: "int", nullable: true),
                    vitoria = table.Column<bool>(type: "bit", nullable: false),
                    pontos = table.Column<int>(type: "int", nullable: false),
                    posicao = table.Column<int>(type: "int", nullable: false),
                    mesaid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pontosJogadoresMesa", x => x.id);
                    table.ForeignKey(
                        name: "FK_pontosJogadoresMesa_jogadores_jogadorid",
                        column: x => x.jogadorid,
                        principalTable: "jogadores",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_pontosJogadoresMesa_mesas_mesaid",
                        column: x => x.mesaid,
                        principalTable: "mesas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_jogadores_JogadorTorneioid",
                table: "jogadores",
                column: "JogadorTorneioid");

            migrationBuilder.CreateIndex(
                name: "IX_jogadores_Torneioid",
                table: "jogadores",
                column: "Torneioid");

            migrationBuilder.CreateIndex(
                name: "IX_jogadoresTorneios_jogadorId",
                table: "jogadoresTorneios",
                column: "jogadorId");

            migrationBuilder.CreateIndex(
                name: "IX_jogadoresTorneios_torneioId",
                table: "jogadoresTorneios",
                column: "torneioId");

            migrationBuilder.CreateIndex(
                name: "IX_mesas_torneioID",
                table: "mesas",
                column: "torneioID");

            migrationBuilder.CreateIndex(
                name: "IX_pontosJogadoresMesa_jogadorid",
                table: "pontosJogadoresMesa",
                column: "jogadorid");

            migrationBuilder.CreateIndex(
                name: "IX_pontosJogadoresMesa_mesaid",
                table: "pontosJogadoresMesa",
                column: "mesaid");

            migrationBuilder.AddForeignKey(
                name: "FK_jogadores_jogadoresTorneios_JogadorTorneioid",
                table: "jogadores",
                column: "JogadorTorneioid",
                principalTable: "jogadoresTorneios",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jogadores_jogadoresTorneios_JogadorTorneioid",
                table: "jogadores");

            migrationBuilder.DropTable(
                name: "pontosJogadoresMesa");

            migrationBuilder.DropTable(
                name: "mesas");

            migrationBuilder.DropTable(
                name: "jogadoresTorneios");

            migrationBuilder.DropTable(
                name: "jogadores");

            migrationBuilder.DropTable(
                name: "torneios");
        }
    }
}
