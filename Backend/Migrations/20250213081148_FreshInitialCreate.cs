using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecruitmentManagement.Migrations
{
    /// <inheritdoc />
    public partial class FreshInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    documentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "InterviewTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    interviewType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "JobStatuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "JobTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    organisationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    skillName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    age = table.Column<int>(type: "int", nullable: true),
                    positionId = table.Column<int>(type: "int", nullable: true),
                    yearsOfExperience = table.Column<int>(type: "int", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    disableReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    resumeLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organisationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organisationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    about = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organisationTypeId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_organisationId",
                        column: x => x.organisationId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_OrganisationTypes_organisationTypeId",
                        column: x => x.organisationTypeId,
                        principalTable: "OrganisationTypes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Positions_positionId",
                        column: x => x.positionId,
                        principalTable: "Positions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "candidateDocs",
                columns: table => new
                {
                    candidateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    documentTypeId = table.Column<int>(type: "int", nullable: false),
                    verifiedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    documentLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidateDocs", x => new { x.candidateId, x.documentTypeId, x.verifiedById });
                    table.ForeignKey(
                        name: "FK_candidateDocs_AspNetUsers_candidateId",
                        column: x => x.candidateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_candidateDocs_AspNetUsers_verifiedById",
                        column: x => x.verifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_candidateDocs_DocumentTypes_documentTypeId",
                        column: x => x.documentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateSkills",
                columns: table => new
                {
                    candidateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    skillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSkills", x => new { x.candidateId, x.skillId });
                    table.ForeignKey(
                        name: "FK_CandidateSkills_AspNetUsers_candidateId",
                        column: x => x.candidateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateSkills_Skills_skillId",
                        column: x => x.skillId,
                        principalTable: "Skills",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "JobOpenings",
                columns: table => new
                {
                    JobOpeningId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jobDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    experienceRequired = table.Column<int>(type: "int", nullable: false),
                    minSalary = table.Column<double>(type: "float", nullable: false),
                    maxSalary = table.Column<double>(type: "float", nullable: false),
                    requiredCandidates = table.Column<int>(type: "int", nullable: false),
                    deadLine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    addtionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    positionId = table.Column<int>(type: "int", nullable: false),
                    organisationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    jobTypeId = table.Column<int>(type: "int", nullable: false),
                    jobStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOpenings", x => x.JobOpeningId);
                    table.ForeignKey(
                        name: "FK_JobOpenings_AspNetUsers_organisationId",
                        column: x => x.organisationId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOpenings_JobStatuses_jobStatusId",
                        column: x => x.jobStatusId,
                        principalTable: "JobStatuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOpenings_JobTypes_jobTypeId",
                        column: x => x.jobTypeId,
                        principalTable: "JobTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOpenings_Positions_positionId",
                        column: x => x.positionId,
                        principalTable: "Positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobCandidates",
                columns: table => new
                {
                    jobOpeningId = table.Column<int>(type: "int", nullable: false),
                    candidateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    noOfInterviewRounds = table.Column<int>(type: "int", nullable: false),
                    isSelected = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCandidates", x => new { x.jobOpeningId, x.candidateId });
                    table.ForeignKey(
                        name: "FK_JobCandidates_AspNetUsers_candidateId",
                        column: x => x.candidateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobCandidates_JobOpenings_jobOpeningId",
                        column: x => x.jobOpeningId,
                        principalTable: "JobOpenings",
                        principalColumn: "JobOpeningId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSkills",
                columns: table => new
                {
                    jobOpeningId = table.Column<int>(type: "int", nullable: false),
                    skillId = table.Column<int>(type: "int", nullable: false),
                    isRequired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkills", x => new { x.jobOpeningId, x.skillId });
                    table.ForeignKey(
                        name: "FK_JobSkills_JobOpenings_jobOpeningId",
                        column: x => x.jobOpeningId,
                        principalTable: "JobOpenings",
                        principalColumn: "JobOpeningId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSkills_Skills_skillId",
                        column: x => x.skillId,
                        principalTable: "Skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledInterviews",
                columns: table => new
                {
                    jobOpeningId = table.Column<int>(type: "int", nullable: false),
                    candidateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    interviewTypeId = table.Column<int>(type: "int", nullable: false),
                    scheduledInterviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    interviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    rating = table.Column<double>(type: "float", nullable: false),
                    feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isCleared = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledInterviews", x => new { x.jobOpeningId, x.candidateId, x.interviewTypeId });
                    table.ForeignKey(
                        name: "FK_ScheduledInterviews_InterviewTypes_interviewTypeId",
                        column: x => x.interviewTypeId,
                        principalTable: "InterviewTypes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ScheduledInterviews_JobCandidates_jobOpeningId_candidateId",
                        columns: x => new { x.jobOpeningId, x.candidateId },
                        principalTable: "JobCandidates",
                        principalColumns: new[] { "jobOpeningId", "candidateId" });
                });

            migrationBuilder.CreateTable(
                name: "RoundHandlers",
                columns: table => new
                {
                    scheduledInterviewJobOpeningId = table.Column<int>(type: "int", nullable: false),
                    scheduledInterviewCandidateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    scheduledInterviewInterviewTypeId = table.Column<int>(type: "int", nullable: false),
                    employeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    roundHandlerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundHandlers", x => new { x.employeeId, x.scheduledInterviewJobOpeningId, x.scheduledInterviewCandidateId, x.scheduledInterviewInterviewTypeId });
                    table.ForeignKey(
                        name: "FK_RoundHandlers_AspNetUsers_employeeId",
                        column: x => x.employeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoundHandlers_ScheduledInterviews_scheduledInterviewJobOpeningId_scheduledInterviewCandidateId_scheduledInterviewInterviewTy~",
                        columns: x => new { x.scheduledInterviewJobOpeningId, x.scheduledInterviewCandidateId, x.scheduledInterviewInterviewTypeId },
                        principalTable: "ScheduledInterviews",
                        principalColumns: new[] { "jobOpeningId", "candidateId", "interviewTypeId" });
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "Admin", null, "Admin", "ADMIN" },
                    { "Candidate", null, "Candidate", "CANDIDATE" },
                    { "Employee", null, "Employee", "EMPLOYEE" },
                    { "Organisation", null, "Organisation", "ORGANISATION" },
                    { "User", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_organisationId",
                table: "AspNetUsers",
                column: "organisationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_organisationTypeId",
                table: "AspNetUsers",
                column: "organisationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_positionId",
                table: "AspNetUsers",
                column: "positionId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_candidateDocs_documentTypeId",
                table: "candidateDocs",
                column: "documentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_candidateDocs_verifiedById",
                table: "candidateDocs",
                column: "verifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkills_skillId",
                table: "CandidateSkills",
                column: "skillId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidates_candidateId",
                table: "JobCandidates",
                column: "candidateId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpenings_jobStatusId",
                table: "JobOpenings",
                column: "jobStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpenings_jobTypeId",
                table: "JobOpenings",
                column: "jobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpenings_organisationId",
                table: "JobOpenings",
                column: "organisationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpenings_positionId",
                table: "JobOpenings",
                column: "positionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_skillId",
                table: "JobSkills",
                column: "skillId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundHandlers_scheduledInterviewJobOpeningId_scheduledInterviewCandidateId_scheduledInterviewInterviewTypeId",
                table: "RoundHandlers",
                columns: new[] { "scheduledInterviewJobOpeningId", "scheduledInterviewCandidateId", "scheduledInterviewInterviewTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledInterviews_interviewTypeId",
                table: "ScheduledInterviews",
                column: "interviewTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "candidateDocs");

            migrationBuilder.DropTable(
                name: "CandidateSkills");

            migrationBuilder.DropTable(
                name: "JobSkills");

            migrationBuilder.DropTable(
                name: "RoundHandlers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "ScheduledInterviews");

            migrationBuilder.DropTable(
                name: "InterviewTypes");

            migrationBuilder.DropTable(
                name: "JobCandidates");

            migrationBuilder.DropTable(
                name: "JobOpenings");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "JobStatuses");

            migrationBuilder.DropTable(
                name: "JobTypes");

            migrationBuilder.DropTable(
                name: "OrganisationTypes");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
