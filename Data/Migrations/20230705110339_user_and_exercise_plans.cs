using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitt.Data.Migrations
{
    /// <inheritdoc />
    public partial class user_and_exercise_plans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserExercisePlanModel",
                columns: table => new
                {
                    ApplicationUserExercisePlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExercisePlanId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserExercisePlanModel", x => x.ApplicationUserExercisePlanId);
                    table.ForeignKey(
                        name: "FK_ApplicationUserExercisePlanModel_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserExercisePlanModel_ExercisePlan_ExercisePlanId",
                        column: x => x.ExercisePlanId,
                        principalTable: "ExercisePlan",
                        principalColumn: "ExercisePlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserExercisePlanModel_ApplicationUserId_ExercisePlanId",
                table: "ApplicationUserExercisePlanModel",
                columns: new[] { "ApplicationUserId", "ExercisePlanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserExercisePlanModel_ExercisePlanId",
                table: "ApplicationUserExercisePlanModel",
                column: "ExercisePlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserExercisePlanModel");
        }
    }
}
