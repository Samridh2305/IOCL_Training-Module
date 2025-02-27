using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOCL_Training_Module.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTrainingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FPR",
                table: "Trainings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FacultyName",
                table: "Trainings",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SafetyTraining",
                table: "Trainings",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Trainings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Trainings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FPR",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "FacultyName",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "SafetyTraining",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Trainings");
        }
    }
}
