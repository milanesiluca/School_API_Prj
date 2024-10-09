using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_School_own_prj.Migrations
{
    /// <inheritdoc />
    public partial class roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "surname",
                table: "Teachers",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Teachers",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Teachers",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Teachers",
                newName: "name");
        }
    }
}
