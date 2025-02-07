using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitmentManagement.Migrations
{
    /// <inheritdoc />
    public partial class DoctypeModelAdded3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_documentTypes",
                table: "documentTypes");

            migrationBuilder.RenameTable(
                name: "documentTypes",
                newName: "DocumentTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentTypes",
                table: "DocumentTypes",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentTypes",
                table: "DocumentTypes");

            migrationBuilder.RenameTable(
                name: "DocumentTypes",
                newName: "documentTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_documentTypes",
                table: "documentTypes",
                column: "id");
        }
    }
}
