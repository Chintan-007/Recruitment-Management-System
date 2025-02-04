using Microsoft.EntityFrameworkCore;

namespace RecruitmentManagement.Models;

public class UserTypeContext : DbContext{
    public UserTypeContext(DbContextOptions<UserTypeContext> options) : base(options){}

    public DbSet<UserType> UserTypes{get; set;}

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserType>().HasKey(u => u.Id);
        base.OnModelCreating(modelBuilder);
    }
}