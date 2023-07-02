using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitt.Data.Migrations
{
    /// <inheritdoc />
    public partial class exercise_plan_and_daily_plan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExercisePlanExerciseDailyPlanModel",
                columns: table => new
                {
                    ExercisePlanExerciseDailyPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExercisePlanId = table.Column<int>(type: "int", nullable: false),
                    ExerciseDailyPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisePlanExerciseDailyPlanModel", x => x.ExercisePlanExerciseDailyPlanId);
                    table.ForeignKey(
                        name: "FK_ExercisePlanExerciseDailyPlanModel_ExerciseDailyPlan_ExerciseDailyPlanId",
                        column: x => x.ExerciseDailyPlanId,
                        principalTable: "ExerciseDailyPlan",
                        principalColumn: "ExerciseDailyPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExercisePlanExerciseDailyPlanModel_ExercisePlan_ExercisePlanId",
                        column: x => x.ExercisePlanId,
                        principalTable: "ExercisePlan",
                        principalColumn: "ExercisePlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExercisePlanExerciseDailyPlanModel_ExerciseDailyPlanId",
                table: "ExercisePlanExerciseDailyPlanModel",
                column: "ExerciseDailyPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisePlanExerciseDailyPlanModel_ExercisePlanId_ExerciseDailyPlanId",
                table: "ExercisePlanExerciseDailyPlanModel",
                columns: new[] { "ExercisePlanId", "ExerciseDailyPlanId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercisePlanExerciseDailyPlanModel");
        }
    }
}
