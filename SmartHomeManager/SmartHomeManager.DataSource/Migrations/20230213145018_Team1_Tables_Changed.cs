using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeManager.DataSource.Migrations
{
    /// <inheritdoc />
    public partial class Team1TablesChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeSecurities_Accounts_AccountId",
                table: "HomeSecurities");

            migrationBuilder.DropIndex(
                name: "IX_HomeSecurities_AccountId",
                table: "HomeSecurities");

            migrationBuilder.AddColumn<Guid>(
                name: "RuleId",
                table: "RuleHistories",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "DeviceAdjustedConfiguration",
                table: "Histories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RuleId",
                table: "RuleHistories");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceAdjustedConfiguration",
                table: "Histories",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_HomeSecurities_AccountId",
                table: "HomeSecurities",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeSecurities_Accounts_AccountId",
                table: "HomeSecurities",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
