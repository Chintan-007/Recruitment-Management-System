using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitmentManagement.Migrations
{
    /// <inheritdoc />
    public partial class create7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_candidateDocs",
                table: "candidateDocs");

            migrationBuilder.AlterColumn<string>(
                name: "verifiedById",
                table: "candidateDocs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_candidateDocs",
                table: "candidateDocs",
                columns: new[] { "candidateId", "documentTypeId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_candidateDocs",
                table: "candidateDocs");

            migrationBuilder.AlterColumn<string>(
                name: "verifiedById",
                table: "candidateDocs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_candidateDocs",
                table: "candidateDocs",
                columns: new[] { "candidateId", "documentTypeId", "verifiedById" });
        }
    }
}
