using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RecruitmentManagement.Models;

public class ApplicationContext : IdentityDbContext{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

    // public DbSet<UserType> UserTypes{get; set;}

    public DbSet<DocumentType> DocumentTypes{get;set;}

    public DbSet<InterviewType> InterviewTypes{get; set;}

    public DbSet<Position> Positions{get; set;}
//-------------------------------------------------------------------------------------------------------
    public DbSet<JobType> JobTypes{get; set;}
    public DbSet<Organisation> Organisations{get; set;}
    public DbSet<OrganisationType> OrganisationTypes{get; set;}

    public DbSet<Users> Users{get;set;}

    public DbSet<Candidate> Candidates{get;set;}
    public DbSet<Skill> Skills{get;set;}
    public DbSet<CandidateSkill> CandidateSkills{get;set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        List<IdentityRole> roles = new List<IdentityRole>{
            new IdentityRole{
                Id = "Admin",
                Name="Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole{
                Id = "User",
                Name = "User",
                NormalizedName = "USER"
            },
            new IdentityRole{
                Id = "Organisation",
                Name = "Organisation",
                NormalizedName = "ORGANISATION"
            },
            new IdentityRole{
                Id="Employee",
                Name="Employee",
                NormalizedName="EMPLOYEE"
            }
        };
        builder.Entity<IdentityRole>().HasData(roles);


        //Relating  OrganisationType with Organisation
        builder.Entity<OrganisationType>()
            .HasMany(ot => ot.organisations)//One orgType has many orgs
            .WithOne(o => o.organisationType)//Each org has One orgType
            .HasForeignKey(o => o.organisationTypeId);

        //Relating Organisation with Employee
        builder.Entity<Organisation>()
            .HasMany(o => o.employees) //One Organisatin many employees
            .WithOne(e => e.organisation)// Each employee has one organisation
            .HasForeignKey(e => e.organisationId);

        //Relating Positoin with Employee
        builder.Entity<Position>()
            .HasMany(p => p.employees) //One positon -> many employees
            .WithOne(e => e.position) // each emp -> one positoin
            .HasForeignKey(e => e.positionId);

        //Relating Position with Candidate
        builder.Entity<Position>()
            .HasMany(p => p.candidates)//One positoin -> many candidates
            .WithOne(c => c.position) // Each candidate -> one position
            .HasForeignKey(c => c.positonId);
        
        //Relating Candidate With skills
        builder.Entity<CandidateSkill>()
            .HasKey(cs => new {cs.candidateId,cs.skillId});

        builder.Entity<CandidateSkill>()
            .HasOne(cs => cs.candidate)
            .WithMany(c => c.candidateSkills)
            .HasForeignKey(cs=>cs.candidateId);

        builder.Entity<CandidateSkill>()
            .HasOne(cs => cs.skill)
            .WithMany(s => s.candidateSkills)
            .HasForeignKey(cs => cs.skillId);
        

    }

}