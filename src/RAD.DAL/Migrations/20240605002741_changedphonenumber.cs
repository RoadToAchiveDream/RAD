using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RAD.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changedphonenumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumer",
                table: "Users",
                newName: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "PhoneNumer");
        }
    }
}
