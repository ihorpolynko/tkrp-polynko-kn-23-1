using CoursesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Course> Courses => Set<Course>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var course = modelBuilder.Entity<Course>();

        course.ToTable("courses");
        course.HasKey(x => x.Id);

        course.Property(x => x.Id).HasColumnName("id");

        course.Property(x => x.Title)
            .HasColumnName("title")
            .HasMaxLength(200)
            .IsRequired();

        course.Property(x => x.Teacher)
            .HasColumnName("teacher")
            .HasMaxLength(150)
            .IsRequired();

        course.Property(x => x.Hours)
            .HasColumnName("hours")
            .IsRequired();

        course.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(500);
    }
}