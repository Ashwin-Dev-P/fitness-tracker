using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitt.Data.Migrations
{
    /// <inheritdoc />
    public partial class create_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageExtension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.ExerciseId);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseDailyPlan",
                columns: table => new
                {
                    ExerciseDailyPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseDailyPlan", x => x.ExerciseDailyPlanId);
                });

            migrationBuilder.CreateTable(
                name: "ExercisePlan",
                columns: table => new
                {
                    ExercisePlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisePlan", x => x.ExercisePlanId);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseType",
                columns: table => new
                {
                    ExerciseTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageExtension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseType", x => x.ExerciseTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseDailyPlanModelExerciseModel",
                columns: table => new
                {
                    ExerciseDailyPlansExerciseDailyPlanId = table.Column<int>(type: "int", nullable: false),
                    ExercisesExerciseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseDailyPlanModelExerciseModel", x => new { x.ExerciseDailyPlansExerciseDailyPlanId, x.ExercisesExerciseId });
                    table.ForeignKey(
                        name: "FK_ExerciseDailyPlanModelExerciseModel_ExerciseDailyPlan_ExerciseDailyPlansExerciseDailyPlanId",
                        column: x => x.ExerciseDailyPlansExerciseDailyPlanId,
                        principalTable: "ExerciseDailyPlan",
                        principalColumn: "ExerciseDailyPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseDailyPlanModelExerciseModel_Exercise_ExercisesExerciseId",
                        column: x => x.ExercisesExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseDailyPlanModelExerciseModel_ExercisesExerciseId",
                table: "ExerciseDailyPlanModelExerciseModel",
                column: "ExercisesExerciseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseDailyPlanModelExerciseModel");

            migrationBuilder.DropTable(
                name: "ExercisePlan");

            migrationBuilder.DropTable(
                name: "ExerciseType");

            migrationBuilder.DropTable(
                name: "ExerciseDailyPlan");

            migrationBuilder.DropTable(
                name: "Exercise");
        }
    }
}
