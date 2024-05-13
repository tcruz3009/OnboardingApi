using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnboardingApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class campo_ativo_Atividade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Atividades",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Atividades");
        }
    }
}
