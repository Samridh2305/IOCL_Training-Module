using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOCL_Training_Module.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeePassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "PasswordHash",
            table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Employees",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
            name: "PasswordHash",
            table: "Employees",
            type: "nvarchar(255)",
            nullable: true);
        }
    }
}
