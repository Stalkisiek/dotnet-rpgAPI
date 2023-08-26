using dotnet_rpg2.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg2.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Skill>().HasData(
            new Skill { Id = 1, Name = "Fireball", Damage = 30 },
            new Skill { Id = 2, Name = "Frenzy", Damage = 30 },
            new Skill { Id = 3, Name = "Snowstorm", Damage = 30 }
        );
    }

    public DbSet<Character> Characters => Set<Character>();
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Weapon> Weapons => Set<Weapon>();
    public DbSet<Skill> Skills => Set<Skill>();

}