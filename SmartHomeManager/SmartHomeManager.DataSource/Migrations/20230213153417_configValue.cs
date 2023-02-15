using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeManager.DataSource.Migrations
{
    /// <inheritdoc />
    public partial class configValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfigurationKey",
                table: "Rules",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfigurationKey",
                table: "Rules");
        }
    }
}
