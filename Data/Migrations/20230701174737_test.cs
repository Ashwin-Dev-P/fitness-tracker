using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitt.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExerciseDailyPlanExerciseModel",
                columns: table => new
                {
                    ExerciseDailyPlanExerciseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    ExerciseDailyPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseDailyPlanExerciseModel", x => x.ExerciseDailyPlanExerciseId);
                    table.ForeignKey(
                        name: "FK_ExerciseDailyPlanExerciseModel_ExerciseDailyPlan_ExerciseDailyPlanId",
                        column: x => x.ExerciseDailyPlanId,
                        principalTable: "ExerciseDailyPlan",
                        principalColumn: "ExerciseDailyPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseDailyPlanExerciseModel_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseDailyPlanExerciseModel_ExerciseDailyPlanId",
                table: "ExerciseDailyPlanExerciseModel",
                column: "ExerciseDailyPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseDailyPlanExerciseModel_ExerciseId",
                table: "ExerciseDailyPlanExerciseModel",
                column: "ExerciseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseDailyPlanExerciseModel");
        }
    }
}
