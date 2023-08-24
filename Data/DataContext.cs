using dotnet_rpg2.Models;

namespace dotnet_rpg2.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<Character> Characters => Set<Character>();
    
    public DbSet<User> Users => Set<User>();
}