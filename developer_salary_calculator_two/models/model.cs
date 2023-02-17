using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace developer_salary_calculator_two;

public class Developer
{
    public int DeveloperId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int WorkedHours { get; set; }
    public double SalaryByHour { get; set; }
}
public class DatabaseContext : DbContext
{
    public DbSet<Developer> Developers { get; set; }
    protected string dbFileName;

    public DatabaseContext( string filename)
    {
        dbFileName=filename;
    }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString: "Filename=" + dbFileName,
            sqliteOptionsAction: op =>
            {
                op.MigrationsAssembly(
                    Assembly.GetExecutingAssembly().FullName
                );
            });
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Developer>().ToTable("Developer");
        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasKey(e => e.DeveloperId);
        });

        base.OnModelCreating(modelBuilder);
    }
}