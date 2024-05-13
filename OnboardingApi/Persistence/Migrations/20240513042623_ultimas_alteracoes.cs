using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnboardingApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ultimas_alteracoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AtividadesOnboardings_Onboarding_OnboardingId",
                table: "AtividadesOnboardings");

            migrationBuilder.DropTable(
                name: "Onboarding");

            migrationBuilder.RenameColumn(
                name: "Tribe",
                table: "Totvers",
                newName: "Tribo");

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
                    UltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Onboardings_NovoTotverId",
                table: "Onboardings",
                column: "NovoTotverId");

            migrationBuilder.CreateIndex(
                name: "IX_Onboardings_PadrinhoId",
                table: "Onboardings",
                column: "PadrinhoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AtividadesOnboardings_Onboardings_OnboardingId",
                table: "AtividadesOnboardings",
                column: "OnboardingId",
                principalTable: "Onboardings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AtividadesOnboardings_Onboardings_OnboardingId",
                table: "AtividadesOnboardings");

            migrationBuilder.DropTable(
                name: "Onboardings");

            migrationBuilder.RenameColumn(
                name: "Tribo",
                table: "Totvers",
                newName: "Tribe");

            migrationBuilder.CreateTable(
                name: "Onboarding",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NovoTotverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PadrinhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlteradoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CriadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusOnboarding = table.Column<int>(type: "int", nullable: false),
                    UltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onboarding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Onboarding_Totvers_NovoTotverId",
                        column: x => x.NovoTotverId,
                        principalTable: "Totvers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Onboarding_Totvers_PadrinhoId",
                        column: x => x.PadrinhoId,
                        principalTable: "Totvers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Onboarding_NovoTotverId",
                table: "Onboarding",
                column: "NovoTotverId");

            migrationBuilder.CreateIndex(
                name: "IX_Onboarding_PadrinhoId",
                table: "Onboarding",
                column: "PadrinhoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AtividadesOnboardings_Onboarding_OnboardingId",
                table: "AtividadesOnboardings",
                column: "OnboardingId",
                principalTable: "Onboarding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
