using Microsoft.EntityFrameworkCore;

namespace RecruitmentManagement.Models;

public class ApplicationContext : DbContext{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

    public DbSet<UserType> UserTypes{get; set;}

    public DbSet<DocumentType> DocumentTypes{get;set;}

    public DbSet<InterviewType> InterviewTypes{get; set;}

}