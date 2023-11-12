using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public DbSet<CipherText> CipherTexts { get; set; }
    public DbSet<PlainText> PlainTexts { get; set; }
    public DbSet<EncType> EncTypes { get; set; }
    public DbSet<Key> Keys { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<EncType>().HasData(
            new EncType{Id = 1, Name = "Cesar"},
            new EncType{Id = 2, Name = "Vigenere"}
            );
    }
}