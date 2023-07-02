using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitt.Data.Migrations
{
    /// <inheritdoc />
    public partial class new2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ExerciseDailyPlanExercise");

            //migrationBuilder.DropTable(
            //    name: "ExerciseDailyPlanModelExerciseModel");

            //migrationBuilder.DropTable(
            //    name: "ExercisePlan");

            //migrationBuilder.DropTable(
            //    name: "ExerciseType");

            //migrationBuilder.DropTable(
            //    name: "ExerciseDailyPlanModel");

            //migrationBuilder.DropTable(
            //    name: "Exercise");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseDailyPlanModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseDailyPlanModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExercisePlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisePlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseDailyPlanExercise",
                columns: table => new
                {
                    ExerciseDailyPlanId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseDailyPlanExercise", x => new { x.ExerciseDailyPlanId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_ExerciseDailyPlanExercise_ExerciseDailyPlanModel_ExerciseDailyPlanId",
                        column: x => x.ExerciseDailyPlanId,
                        principalTable: "ExerciseDailyPlanModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseDailyPlanExercise_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseDailyPlanModelExerciseModel",
                columns: table => new
                {
                    ExerciseDailyPlansId = table.Column<int>(type: "int", nullable: false),
                    ExercisesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseDailyPlanModelExerciseModel", x => new { x.ExerciseDailyPlansId, x.ExercisesId });
                    table.ForeignKey(
                        name: "FK_ExerciseDailyPlanModelExerciseModel_ExerciseDailyPlanModel_ExerciseDailyPlansId",
                        column: x => x.ExerciseDailyPlansId,
                        principalTable: "ExerciseDailyPlanModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseDailyPlanModelExerciseModel_Exercise_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseDailyPlanExercise_ExerciseId",
                table: "ExerciseDailyPlanExercise",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseDailyPlanModelExerciseModel_ExercisesId",
                table: "ExerciseDailyPlanModelExerciseModel",
                column: "ExercisesId");
        }
    }
}
