using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitt.Data.Migrations
{
    /// <inheritdoc />
    public partial class calorie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calorie",
                columns: table => new
                {
                    CalorieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalorieCount = table.Column<int>(type: "int", nullable: false),
                    CalorieConsumptionDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calorie", x => x.CalorieId);
                    table.ForeignKey(
                        name: "FK_Calorie_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calorie_ApplicationUserId",
                table: "Calorie",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calorie");
        }
    }
}
