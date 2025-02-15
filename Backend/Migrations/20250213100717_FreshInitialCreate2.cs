using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitmentManagement.Migrations
{
    /// <inheritdoc />
    public partial class FreshInitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobSkills",
                table: "JobSkills");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobSkills",
                table: "JobSkills",
                columns: new[] { "jobOpeningId", "skillId", "isRequired" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobSkills",
                table: "JobSkills");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobSkills",
                table: "JobSkills",
                columns: new[] { "jobOpeningId", "skillId" });
        }
    }
}
