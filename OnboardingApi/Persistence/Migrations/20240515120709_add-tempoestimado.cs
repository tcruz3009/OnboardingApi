using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnboardingApi.Migrations
{
    /// <inheritdoc />
    public partial class addtempoestimado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TempoEstimado",
                table: "Atividades",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempoEstimado",
                table: "Atividades");
        }
    }
}
