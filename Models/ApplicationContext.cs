using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RecruitmentManagement.Models;

public class ApplicationContext : IdentityDbContext{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

    // public DbSet<UserType> UserTypes{get; set;}
    public DbSet<Candidate> Candidates{get;set;}
    public DbSet<CandidateDocs> candidateDocs{get; set;}
    public DbSet<CandidateSkill> CandidateSkills{get;set;}
    public DbSet<DocumentType> DocumentTypes{get;set;}
    public DbSet<Employee> Employees{get; set;}
    public DbSet<InterviewType> InterviewTypes{get; set;}
    public DbSet<JobCandidate> JobCandidates{get; set;}
    public DbSet<JobOpening> JobOpenings{get; set;}
    public DbSet<JobSkill> JobSkills{get; set;}
    public DbSet<JobStatus> JobStatuses{get; set;}
    public DbSet<JobType> JobTypes{get; set;}
    public DbSet<Organisation> Organisations{get; set;}
    public DbSet<OrganisationType> OrganisationTypes{get; set;}
    public DbSet<Position> Positions{get; set;}
    public DbSet<RoundHandler> RoundHandlers{get; set;}
    public DbSet<ScheduledInterview> ScheduledInterviews{get; set;}
    public DbSet<Skill> Skills{get; set;}
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
            },
            new IdentityRole{
                Id="Candidate",
                Name="Candidate",
                NormalizedName="CANDIDATE"
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

        //Relating Positoin with User
        builder.Entity<Position>()
            .HasMany(p => p.users) //One positon -> many users
            .WithOne(e => e.position) // each user -> one positoin
            .HasForeignKey(e => e.positionId);

        
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
            .HasForeignKey(cs => cs.skillId)
            .OnDelete(DeleteBehavior.NoAction);
        

        //Relating Candidate, DocumentType and employee(verfier)
        builder.Entity<CandidateDocs>()
            .HasKey(cd => new{cd.candidateId,cd.documentTypeId,cd.verifiedById});
        
        builder.Entity<CandidateDocs>()
            .HasOne(cd => cd.documentType)
            .WithMany(d=> d.candidateDocs)
            .HasForeignKey(cd=> cd.documentTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<CandidateDocs>()
            .HasOne(cd=>cd.candidate)
            .WithMany(c=> c.candidateDocs)
            .HasForeignKey(cd=> cd.candidateId);
    
        builder.Entity<CandidateDocs>()
            .HasOne(cd=>cd.verifiedBy)
            .WithMany(e=> e.candidateDocs)
            .HasForeignKey(cd=> cd.verifiedById)
            .OnDelete(DeleteBehavior.NoAction);
    

        //Relating Position to JobOpening
        builder.Entity<Position>()
            .HasMany(p => p.jobOpenings)
            .WithOne(jo => jo.position)
            .HasForeignKey(jo => jo.positionId);

        //Relating Organisation to JobOpening
        builder.Entity<Organisation>()
            .HasMany(o => o.jobOpenings)
            .WithOne(jo => jo.organisation)
            .HasForeignKey(jo => jo.organisationId);

        //Relating JobType to JobOpening
        builder.Entity<JobType>()
            .HasMany(jt => jt.jobOpenings)
            .WithOne(jo => jo.jobType)
            .HasForeignKey(jo => jo.jobTypeId);

        //Relating JobType to JobOpening
        builder.Entity<JobStatus>()
            .HasMany(js => js.jobOpenings)
            .WithOne(jo => jo.jobStatus)
            .HasForeignKey(jo => jo.jobStatusId);

        //Relating jobskills with jobOpening and Skill
        builder.Entity<JobSkill>()
            .HasKey(js => new {js.jobOpeningId, js.skillId});

        builder.Entity<JobSkill>()
            .HasOne(js => js.jobOpening)
            .WithMany(jo=>jo.jobSkills)
            .HasForeignKey(js=>js.jobOpeningId);

        builder.Entity<JobSkill>()
            .HasOne(js=>js.skill)
            .WithMany(s=> s.jobSkills)
            .HasForeignKey(js => js.skillId);


        //Relating jobCandidate with jobopening and candidate
        builder.Entity<JobCandidate>()
            .HasKey(jc => new{jc.jobOpeningId,jc.candidateId});
        
        builder.Entity<JobCandidate>()
            .HasOne(jc=> jc.jobOpening)
            .WithMany(jo => jo.jobCandidates)
            .HasForeignKey(jc=>jc.jobOpeningId);
        
        builder.Entity<JobCandidate>()
            .HasOne(jc=>jc.candidate)
            .WithMany(c=> c.jobCandidates)
            .HasForeignKey(jc=>jc.candidateId)
            .OnDelete(DeleteBehavior.NoAction);

        //Relating scheduledInterview with jobCandidate and Interview type
        builder.Entity<ScheduledInterview>()
            .HasKey(si => new { si.jobOpeningId, si.candidateId, si.interviewTypeId });

        builder.Entity<ScheduledInterview>()
            .HasOne(si => si.jobCandidate)
            .WithMany(jc => jc.scheduledInterviews)
            .HasForeignKey(si => new { si.jobOpeningId, si.candidateId })
            .OnDelete(DeleteBehavior.NoAction);  // Prevent cascading delete here

        builder.Entity<ScheduledInterview>()
            .HasOne(si => si.interviewType)
            .WithMany(it => it.scheduledInterviews)
            .HasForeignKey(si => si.interviewTypeId)
            .OnDelete(DeleteBehavior.NoAction);  // Prevent cascading delete here

        //Relating Roundhandler with scheduledInterview and Employee
        builder.Entity<RoundHandler>()
            .HasKey(rh => new{rh.employeeId,rh.scheduledInterviewJobOpeningId,rh.scheduledInterviewCandidateId,rh.scheduledInterviewInterviewTypeId});

        builder.Entity<RoundHandler>()
            .HasOne(rh=>rh.scheduledInterview)
            .WithMany(si => si.roundHandlers)
            .HasForeignKey(rh=>new{rh.scheduledInterviewJobOpeningId,rh.scheduledInterviewCandidateId,rh.scheduledInterviewInterviewTypeId})
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.Entity<RoundHandler>()
            .HasOne(rh=>rh.employee)
            .WithMany(e=>e.roundHandlers)
            .HasForeignKey(rh=>rh.employeeId)
            .OnDelete(DeleteBehavior.NoAction);
    }

}