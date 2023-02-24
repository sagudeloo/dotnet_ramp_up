using Microsoft.EntityFrameworkCore;

namespace authentication_jwt.Models;

public enum UserRole
{
    Reader,
    Contributor,
    Manager
}


public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserRole Role { get; set; }
    public bool IsActiveRole { get; set; } = true;
}

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    protected readonly IConfiguration _configuration;

    public DatabaseContext( IConfiguration configuration)
    {
        _configuration=configuration;
    }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString: _configuration.GetConnectionString("UsersDB"));
        
        base.OnConfiguring(optionsBuilder);
    }
}