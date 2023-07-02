using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitt.Data.Migrations
{
    /// <inheritdoc />
    public partial class exercise_plan_combined_with_exercise_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseDailyPlanExerciseModel_ExerciseDailyPlan_ExerciseDailyPlanId",
                table: "ExerciseDailyPlanExerciseModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseDailyPlanExerciseModel_Exercise_ExerciseId",
                table: "ExerciseDailyPlanExerciseModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ExercisePlan_ExerciseType_ExerciseTypeModelExerciseTypeId",
                table: "ExercisePlan");

            migrationBuilder.DropTable(
                name: "ExerciseTypeExercisePlanModel");

            migrationBuilder.DropIndex(
                name: "IX_ExercisePlan_ExerciseTypeModelExerciseTypeId",
                table: "ExercisePlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseDailyPlanExerciseModel",
                table: "ExerciseDailyPlanExerciseModel");

            migrationBuilder.DropColumn(
                name: "ExerciseTypeModelExerciseTypeId",
                table: "ExercisePlan");

            migrationBuilder.RenameTable(
                name: "ExerciseDailyPlanExerciseModel",
                newName: "ExerciseDailyPlanExercise");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseDailyPlanExerciseModel_ExerciseId_ExerciseDailyPlanId",
                table: "ExerciseDailyPlanExercise",
                newName: "IX_ExerciseDailyPlanExercise_ExerciseId_ExerciseDailyPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseDailyPlanExerciseModel_ExerciseDailyPlanId",
                table: "ExerciseDailyPlanExercise",
                newName: "IX_ExerciseDailyPlanExercise_ExerciseDailyPlanId");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseTypeId",
                table: "ExercisePlan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseDailyPlanExercise",
                table: "ExerciseDailyPlanExercise",
                column: "ExerciseDailyPlanExerciseId");

            migrationBuilder.CreateTable(
                name: "ExerciseDailyPlanModelExercisePlanModel",
                columns: table => new
                {
                    ExerciseDailyPlansExerciseDailyPlanId = table.Column<int>(type: "int", nullable: false),
                    ExercisePlanModelExercisePlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseDailyPlanModelExercisePlanModel", x => new { x.ExerciseDailyPlansExerciseDailyPlanId, x.ExercisePlanModelExercisePlanId });
                    table.ForeignKey(
                        name: "FK_ExerciseDailyPlanModelExercisePlanModel_ExerciseDailyPlan_ExerciseDailyPlansExerciseDailyPlanId",
                        column: x => x.ExerciseDailyPlansExerciseDailyPlanId,
                        principalTable: "ExerciseDailyPlan",
                        principalColumn: "ExerciseDailyPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseDailyPlanModelExercisePlanModel_ExercisePlan_ExercisePlanModelExercisePlanId",
                        column: x => x.ExercisePlanModelExercisePlanId,
                        principalTable: "ExercisePlan",
                        principalColumn: "ExercisePlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExercisePlan_ExerciseTypeId",
                table: "ExercisePlan",
                column: "ExerciseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseDailyPlanModelExercisePlanModel_ExercisePlanModelExercisePlanId",
                table: "ExerciseDailyPlanModelExercisePlanModel",
                column: "ExercisePlanModelExercisePlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseDailyPlanExercise_ExerciseDailyPlan_ExerciseDailyPlanId",
                table: "ExerciseDailyPlanExercise",
                column: "ExerciseDailyPlanId",
                principalTable: "ExerciseDailyPlan",
                principalColumn: "ExerciseDailyPlanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseDailyPlanExercise_Exercise_ExerciseId",
                table: "ExerciseDailyPlanExercise",
                column: "ExerciseId",
                principalTable: "Exercise",
                principalColumn: "ExerciseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExercisePlan_ExerciseType_ExerciseTypeId",
                table: "ExercisePlan",
                column: "ExerciseTypeId",
                principalTable: "ExerciseType",
                principalColumn: "ExerciseTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseDailyPlanExercise_ExerciseDailyPlan_ExerciseDailyPlanId",
                table: "ExerciseDailyPlanExercise");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseDailyPlanExercise_Exercise_ExerciseId",
                table: "ExerciseDailyPlanExercise");

            migrationBuilder.DropForeignKey(
                name: "FK_ExercisePlan_ExerciseType_ExerciseTypeId",
                table: "ExercisePlan");

            migrationBuilder.DropTable(
                name: "ExerciseDailyPlanModelExercisePlanModel");

            migrationBuilder.DropIndex(
                name: "IX_ExercisePlan_ExerciseTypeId",
                table: "ExercisePlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseDailyPlanExercise",
                table: "ExerciseDailyPlanExercise");

            migrationBuilder.DropColumn(
                name: "ExerciseTypeId",
                table: "ExercisePlan");

            migrationBuilder.RenameTable(
                name: "ExerciseDailyPlanExercise",
                newName: "ExerciseDailyPlanExerciseModel");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseDailyPlanExercise_ExerciseId_ExerciseDailyPlanId",
                table: "ExerciseDailyPlanExerciseModel",
                newName: "IX_ExerciseDailyPlanExerciseModel_ExerciseId_ExerciseDailyPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseDailyPlanExercise_ExerciseDailyPlanId",
                table: "ExerciseDailyPlanExerciseModel",
                newName: "IX_ExerciseDailyPlanExerciseModel_ExerciseDailyPlanId");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseTypeModelExerciseTypeId",
                table: "ExercisePlan",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseDailyPlanExerciseModel",
                table: "ExerciseDailyPlanExerciseModel",
                column: "ExerciseDailyPlanExerciseId");

            migrationBuilder.CreateTable(
                name: "ExerciseTypeExercisePlanModel",
                columns: table => new
                {
                    ExerciseTypeExercisePlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExercisePlanId = table.Column<int>(type: "int", nullable: false),
                    ExerciseTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTypeExercisePlanModel", x => x.ExerciseTypeExercisePlanId);
                    table.ForeignKey(
                        name: "FK_ExerciseTypeExercisePlanModel_ExercisePlan_ExercisePlanId",
                        column: x => x.ExercisePlanId,
                        principalTable: "ExercisePlan",
                        principalColumn: "ExercisePlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseTypeExercisePlanModel_ExerciseType_ExerciseTypeId",
                        column: x => x.ExerciseTypeId,
                        principalTable: "ExerciseType",
                        principalColumn: "ExerciseTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExercisePlan_ExerciseTypeModelExerciseTypeId",
                table: "ExercisePlan",
                column: "ExerciseTypeModelExerciseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTypeExercisePlanModel_ExercisePlanId",
                table: "ExerciseTypeExercisePlanModel",
                column: "ExercisePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTypeExercisePlanModel_ExerciseTypeId",
                table: "ExerciseTypeExercisePlanModel",
                column: "ExerciseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseDailyPlanExerciseModel_ExerciseDailyPlan_ExerciseDailyPlanId",
                table: "ExerciseDailyPlanExerciseModel",
                column: "ExerciseDailyPlanId",
                principalTable: "ExerciseDailyPlan",
                principalColumn: "ExerciseDailyPlanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseDailyPlanExerciseModel_Exercise_ExerciseId",
                table: "ExerciseDailyPlanExerciseModel",
                column: "ExerciseId",
                principalTable: "Exercise",
                principalColumn: "ExerciseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExercisePlan_ExerciseType_ExerciseTypeModelExerciseTypeId",
                table: "ExercisePlan",
                column: "ExerciseTypeModelExerciseTypeId",
                principalTable: "ExerciseType",
                principalColumn: "ExerciseTypeId");
        }
    }
}
