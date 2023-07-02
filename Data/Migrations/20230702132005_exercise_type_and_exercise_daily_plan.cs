using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitt.Data.Migrations
{
    /// <inheritdoc />
    public partial class exercise_type_and_exercise_daily_plan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExerciseTypeExerciseDailyPlanModel",
                columns: table => new
                {
                    ExerciseTypeExerciseDailyPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseTypeId = table.Column<int>(type: "int", nullable: false),
                    ExerciseDailyPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTypeExerciseDailyPlanModel", x => x.ExerciseTypeExerciseDailyPlanId);
                    table.ForeignKey(
                        name: "FK_ExerciseTypeExerciseDailyPlanModel_ExerciseDailyPlan_ExerciseDailyPlanId",
                        column: x => x.ExerciseDailyPlanId,
                        principalTable: "ExerciseDailyPlan",
                        principalColumn: "ExerciseDailyPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseTypeExerciseDailyPlanModel_ExerciseType_ExerciseTypeId",
                        column: x => x.ExerciseTypeId,
                        principalTable: "ExerciseType",
                        principalColumn: "ExerciseTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTypeExerciseDailyPlanModel_ExerciseDailyPlanId",
                table: "ExerciseTypeExerciseDailyPlanModel",
                column: "ExerciseDailyPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTypeExerciseDailyPlanModel_ExerciseTypeId",
                table: "ExerciseTypeExerciseDailyPlanModel",
                column: "ExerciseTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseTypeExerciseDailyPlanModel");
        }
    }
}
