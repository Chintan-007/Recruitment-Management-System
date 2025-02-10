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

    public DbSet<OrganisationType> OrganisationTypes{get; set;}

    public DbSet<Organisation> Organisations{get; set;}

    public DbSet<Users> Users{get;set;}

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
            }
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }

}