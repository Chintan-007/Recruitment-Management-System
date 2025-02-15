using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitmentManagement.Migrations
{
    /// <inheritdoc />
    public partial class FreshInitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isFiltered",
                table: "JobCandidates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isFiltered",
                table: "JobCandidates");
        }
    }
}
