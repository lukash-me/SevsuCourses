using System.Data.Common;
using labs_ud.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Task = labs_ud.Application.Entities.Task;

namespace labs_ud.Application.Repositories;

public class ApplicationDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<Course> Course { get; set; }
    public DbSet<Teacher> Teacher { get; set; }
    public DbSet<Mentor> Mentor { get; set; }
    public DbSet<Theme> Theme { get; set; }
    public DbSet<Task> Task { get; set; }
    public DbSet<Solution> Solution { get; set; }
    public DbSet<Group> Group { get; set; }
    public DbSet<Student> Student { get; set; }
    public DbSet<Answer> Answer { get; set; }
    public DbSet<StudentGroup> StudentGroup { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(configuration.GetConnectionString(nameof(DbConnection)))
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.Entity<Course>().ToTable("Course", "admin");
        modelBuilder.Entity<Teacher>().ToTable("Teacher", "admin");
        modelBuilder.Entity<Mentor>().ToTable("Mentor", "admin");
        modelBuilder.Entity<Theme>().ToTable("Theme", "admin");
        modelBuilder.Entity<Task>().ToTable("Task", "admin");
        modelBuilder.Entity<Solution>().ToTable("Solution", "admin");
        modelBuilder.Entity<Group>().ToTable("Group", "admin");
        modelBuilder.Entity<Student>().ToTable("Student", "admin");
        modelBuilder.Entity<StudentGroup>().ToTable("Student_Group", "admin");
        modelBuilder.Entity<Answer>().ToTable("Answer", "admin");
        base.OnModelCreating(modelBuilder);
    }
}