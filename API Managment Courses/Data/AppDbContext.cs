
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Lesson> Lessons { get; set; }

    public DbSet<CourseEnrollment> CourseEnrollments { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Lessons)
            .WithOne(l => l.Course);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfile>(p => p.ID);

        modelBuilder.Entity<User>()
               .Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(50);

        modelBuilder.Entity<CourseEnrollment>()
            .HasKey(c => new { c.UserID, c.CourseID });

        modelBuilder.Entity<CourseEnrollment>()
            .HasOne(c => c.User)
            .WithMany(u => u.CourseEnrollments)
            .HasForeignKey(c => c.UserID);

        modelBuilder.Entity<CourseEnrollment>()
            .HasOne(c => c.Course)
            .WithMany(e => e.CourseEnrollments)
            .HasForeignKey(c => c.CourseID);

        modelBuilder.Entity<UserProfile>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<UserProfile>()
            .Property(p => p.Surname)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Lesson>()
            .Property(l => l.Title)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Lesson>()
            .Property(l => l.Description)
            .IsRequired()
            .HasMaxLength(200);

    }
}

