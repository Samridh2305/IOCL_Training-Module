using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOCL_Training_Module.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmpDepartment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmpDesignation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sex = table.Column<string>(type: "char(1)", nullable: false),
                    Reporting = table.Column<bool>(type: "bit", nullable: false),
                    ControllingEmp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmpNo);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    TrainingID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TrainingName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Validity = table.Column<int>(type: "int", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.TrainingID);
                });

            migrationBuilder.CreateTable(
                name: "Reporting",
                columns: table => new
                {
                    SrNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpNo = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Reporting = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reporting", x => x.SrNo);
                    table.ForeignKey(
                        name: "FK_Reporting_Employees_EmpNo",
                        column: x => x.EmpNo,
                        principalTable: "Employees",
                        principalColumn: "EmpNo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reporting_Employees_Reporting",
                        column: x => x.Reporting,
                        principalTable: "Employees",
                        principalColumn: "EmpNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompletedTrainings",
                columns: table => new
                {
                    SrNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompletedTraining = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedTrainings", x => x.SrNo);
                    table.ForeignKey(
                        name: "FK_CompletedTrainings_Employees_EmpNo",
                        column: x => x.EmpNo,
                        principalTable: "Employees",
                        principalColumn: "EmpNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompletedTrainings_Trainings_CompletedTraining",
                        column: x => x.CompletedTraining,
                        principalTable: "Trainings",
                        principalColumn: "TrainingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotCompletedTrainings",
                columns: table => new
                {
                    SNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpNo = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotCompletedTrainings", x => x.SNo);
                    table.ForeignKey(
                        name: "FK_NotCompletedTrainings_Employees_EmpNo",
                        column: x => x.EmpNo,
                        principalTable: "Employees",
                        principalColumn: "EmpNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotCompletedTrainings_Trainings_Code",
                        column: x => x.Code,
                        principalTable: "Trainings",
                        principalColumn: "TrainingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecurringTraining",
                columns: table => new
                {
                    SrNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpNo = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TrainingID = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringTraining", x => x.SrNo);
                    table.ForeignKey(
                        name: "FK_RecurringTraining_Employees_EmpNo",
                        column: x => x.EmpNo,
                        principalTable: "Employees",
                        principalColumn: "EmpNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecurringTraining_Trainings_TrainingID",
                        column: x => x.TrainingID,
                        principalTable: "Trainings",
                        principalColumn: "TrainingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedTrainings_CompletedTraining",
                table: "CompletedTrainings",
                column: "CompletedTraining");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedTrainings_EmpNo",
                table: "CompletedTrainings",
                column: "EmpNo");

            migrationBuilder.CreateIndex(
                name: "IX_NotCompletedTrainings_Code",
                table: "NotCompletedTrainings",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_NotCompletedTrainings_EmpNo",
                table: "NotCompletedTrainings",
                column: "EmpNo");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTraining_EmpNo",
                table: "RecurringTraining",
                column: "EmpNo");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTraining_TrainingID",
                table: "RecurringTraining",
                column: "TrainingID");

            migrationBuilder.CreateIndex(
                name: "IX_Reporting_EmpNo",
                table: "Reporting",
                column: "EmpNo");

            migrationBuilder.CreateIndex(
                name: "IX_Reporting_Reporting",
                table: "Reporting",
                column: "Reporting");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedTrainings");

            migrationBuilder.DropTable(
                name: "NotCompletedTrainings");

            migrationBuilder.DropTable(
                name: "RecurringTraining");

            migrationBuilder.DropTable(
                name: "Reporting");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
