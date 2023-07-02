using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitt.Data.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExerciseTypeExercisePlanModel_ExerciseTypeId_ExercisePlanId",
                table: "ExerciseTypeExercisePlanModel");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseTypeModelExerciseTypeId",
                table: "ExercisePlan",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTypeExercisePlanModel_ExerciseTypeId",
                table: "ExerciseTypeExercisePlanModel",
                column: "ExerciseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisePlan_ExerciseTypeModelExerciseTypeId",
                table: "ExercisePlan",
                column: "ExerciseTypeModelExerciseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExercisePlan_ExerciseType_ExerciseTypeModelExerciseTypeId",
                table: "ExercisePlan",
                column: "ExerciseTypeModelExerciseTypeId",
                principalTable: "ExerciseType",
                principalColumn: "ExerciseTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExercisePlan_ExerciseType_ExerciseTypeModelExerciseTypeId",
                table: "ExercisePlan");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseTypeExercisePlanModel_ExerciseTypeId",
                table: "ExerciseTypeExercisePlanModel");

            migrationBuilder.DropIndex(
                name: "IX_ExercisePlan_ExerciseTypeModelExerciseTypeId",
                table: "ExercisePlan");

            migrationBuilder.DropColumn(
                name: "ExerciseTypeModelExerciseTypeId",
                table: "ExercisePlan");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTypeExercisePlanModel_ExerciseTypeId_ExercisePlanId",
                table: "ExerciseTypeExercisePlanModel",
                columns: new[] { "ExerciseTypeId", "ExercisePlanId" },
                unique: true);
        }
    }
}
