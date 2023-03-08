using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace web_api.Models;

public enum DeveloperType
{
    Junior,
    Intermediate,
    Senior,
    Lead,

}
public class Developer
{
    public int developerId { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    [NotMapped]
    public string fullName { get {
        return $"{firstName} {lastName}";
    }}
    public int age { get; set; }
    public DeveloperType type { get; set; }
    public int workedHours { get; set; }
    public double salaryByHour { get; set; }
    public string email { get; set; }
    

}

public class DeveloperValidator: AbstractValidator<Developer>
{
    public DeveloperValidator()
    {
        RuleFor(developer => developer.firstName).NotEmpty().MinimumLength(3).MaximumLength(20);
        RuleFor(developer => developer.lastName).NotEmpty().MinimumLength(3).MaximumLength(30);
        RuleFor(developer => developer.age).NotEmpty().GreaterThan(10);
        RuleFor(developer => developer.workedHours).NotEmpty().GreaterThan(30).LessThan(50);
        RuleFor(developer => developer.salaryByHour).NotEmpty().GreaterThan(13).LessThan(50);
    }
}

public class DatabaseContext : DbContext
{
    public DbSet<Developer> Developers { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    // protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlite(connectionString: "Filename=" + dbFileName,
    //         sqliteOptionsAction: op =>
    //         {
    //             op.MigrationsAssembly(
    //                 Assembly.GetExecutingAssembly().FullName
    //             );
    //         });
        
    //     base.OnConfiguring(optionsBuilder);
    // }

    protected override void OnModelCreating( ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Developer>().ToTable("Developer");
        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasKey(e => e.developerId);
            entity.HasIndex(e => e.email).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}