
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

        modelBuilder.Entity<User>()
            .HasData(
            new User { ID = 1, Email = "kamils3542@gmail.com" },
            new User { ID = 2, Email = "kamils35422@gmail.com" },
            new User { ID = 3, Email = "kamils354222@gmail.com" }
            );

        modelBuilder.Entity<UserProfile>()
            .HasData(
                new UserProfile { ID = 1, Name = "Kamil", Surname = "Sołtys" },
                new UserProfile { ID = 2, Name = "Andrzej", Surname = "Lepper"},
                new UserProfile { ID = 3, Name = "Tomek", Surname = "Wielki"}
            );


        modelBuilder.Entity<Course>()
            .HasData(
            new Course { ID = 1, Title = "Przykładowy tytuł kursu", Description = "Przykładowy opis kursu" },
            new Course { ID = 2, Title = "Przykładowy tytuł drugiego kursu", Description = "Przykładowy drugiego opis kursu" },
            new Course { ID = 3, Title = "Przykładowy tytuł trzeciego kursu", Description = "Przykładowy trzeciego opis kursu" },
            new Course { ID = 4, Title = "Przykładowy tytuł czwartego kursu", Description = "Przykładowy opis czwartego kursu" },
            new Course { ID = 5, Title = "Przykładowy tytuł piątego kursu", Description = "Przykładowy opis czwartego kursu" }
            );

        modelBuilder.Entity<CourseEnrollment>()
            .HasData(new CourseEnrollment { UserID = 1, CourseID = 1 }, new CourseEnrollment { UserID = 2, CourseID = 2 }, new CourseEnrollment { UserID = 3, CourseID = 3 });


        modelBuilder.Entity<Lesson>()
          .HasData(
          new Lesson { ID = 1,Title = "Przykładowy tytuł lekcji", Description = "Przykładowy opis lekcji", CourseID = 1 },
          new Lesson { ID = 2,Title = "Przykładowy tytuł drugiego lekcji", Description = "Przykładowy drugiego opis lekcji", CourseID = 2 },
          new Lesson { ID = 3,Title = "Przykładowy tytuł trzeciego lekcji", Description = "Przykładowy trzeciego opis lekcji", CourseID = 3 },
          new Lesson { ID = 4, Title = "Przykładowy tytuł czwartego lekcji", Description = "Przykładowy czwartego opis lekcji", CourseID = 4 },
          new Lesson { ID = 5, Title = "Przykładowy tytuł piątego lekcji", Description = "Przykładowy piątego opis lekcji", CourseID = 5 }
          );



    }




}

