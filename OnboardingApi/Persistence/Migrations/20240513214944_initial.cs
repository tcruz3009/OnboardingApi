using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnboardingApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atividades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Obrigatório = table.Column<bool>(type: "bit", nullable: false),
                    ComoFazer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Classificacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CriadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AlteradoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Totvers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioRede = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tribo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CriadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AlteradoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Totvers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Onboardings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PadrinhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NovoTotverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusOnboarding = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CriadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AlteradoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onboardings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Onboardings_Totvers_NovoTotverId",
                        column: x => x.NovoTotverId,
                        principalTable: "Totvers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Onboardings_Totvers_PadrinhoId",
                        column: x => x.PadrinhoId,
                        principalTable: "Totvers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AtividadesOnboardings",
                columns: table => new
                {
                    OnboardingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AtividadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusAtividade = table.Column<int>(type: "int", nullable: false),
                    Comentário = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CriadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AlteradoPor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtividadesOnboardings", x => new { x.OnboardingId, x.AtividadeId });
                    table.ForeignKey(
                        name: "FK_AtividadesOnboardings_Atividades_AtividadeId",
                        column: x => x.AtividadeId,
                        principalTable: "Atividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtividadesOnboardings_Onboardings_OnboardingId",
                        column: x => x.OnboardingId,
                        principalTable: "Onboardings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtividadesOnboardings_AtividadeId",
                table: "AtividadesOnboardings",
                column: "AtividadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Onboardings_NovoTotverId",
                table: "Onboardings",
                column: "NovoTotverId");

            migrationBuilder.CreateIndex(
                name: "IX_Onboardings_PadrinhoId",
                table: "Onboardings",
                column: "PadrinhoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtividadesOnboardings");

            migrationBuilder.DropTable(
                name: "Atividades");

            migrationBuilder.DropTable(
                name: "Onboardings");

            migrationBuilder.DropTable(
                name: "Totvers");
        }
    }
}
