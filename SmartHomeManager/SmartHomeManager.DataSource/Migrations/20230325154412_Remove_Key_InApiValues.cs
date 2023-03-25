using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeManager.DataSource.Migrations
{
    /// <inheritdoc />
    public partial class RemoveKeyInApiValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_APIValues",
                table: "APIValues");

            migrationBuilder.CreateIndex(
                name: "IX_APIValues_APIKeyType",
                table: "APIValues",
                column: "APIKeyType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_APIValues_APIKeyType",
                table: "APIValues");

            migrationBuilder.AddPrimaryKey(
                name: "PK_APIValues",
                table: "APIValues",
                column: "APIKeyType");
        }
    }
}
