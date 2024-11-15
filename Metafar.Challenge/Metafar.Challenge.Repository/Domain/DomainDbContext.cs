using Metafar.Challenge.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Metafar.Challenge.Repository.Domain;

public class DomainDbContext: DbContext
{
    public DbSet<CardModel> Cards { get; set; }
    public DbSet<AccountModel> Accounts { get; set; }
    public DbSet<Operation> Operations { get; set; }
    public DbSet<UserModel> Users { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Additional configurations can be added here
        /*modelBuilder.Entity<CardModel>().HasKey(c => c.CardId);
        modelBuilder.Entity<CardModel>().Property(c => c.CardNumber).IsRequired();
        */
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=metafar.challenge.db;Persist Security Info=False;User ID=sa;Password=Password12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
    }
}