using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMAPredictor.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FighterNameIsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Fighters_Name",
                table: "Fighters",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Fighters_Name",
                table: "Fighters");
        }
    }
}
