using Metafar.Challenge.Entity;
using Microsoft.EntityFrameworkCore;

namespace Metafar.Challenge.Repository;

public class RepositoryDbContext: DbContext
{
    public DbSet<CardEntity> Cards { get; set; }
    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<OperationEntity> Operations { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    

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