using Microsoft.EntityFrameworkCore;

namespace background_service;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TimeZone { get; set; }
}

public class DatabaseContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    protected readonly IConfiguration _configuration;

    public DatabaseContext( IConfiguration configuration)
    {
        _configuration=configuration;
    }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString: _configuration.GetConnectionString("CitiesDB"));
        
        base.OnConfiguring(optionsBuilder);
    }
}