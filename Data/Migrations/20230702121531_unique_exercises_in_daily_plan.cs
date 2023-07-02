using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitt.Data.Migrations
{
    /// <inheritdoc />
    public partial class unique_exercises_in_daily_plan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExerciseDailyPlanExerciseModel_ExerciseId",
                table: "ExerciseDailyPlanExerciseModel");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseDailyPlanExerciseModel_ExerciseId_ExerciseDailyPlanId",
                table: "ExerciseDailyPlanExerciseModel",
                columns: new[] { "ExerciseId", "ExerciseDailyPlanId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExerciseDailyPlanExerciseModel_ExerciseId_ExerciseDailyPlanId",
                table: "ExerciseDailyPlanExerciseModel");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseDailyPlanExerciseModel_ExerciseId",
                table: "ExerciseDailyPlanExerciseModel",
                column: "ExerciseId");
        }
    }
}
