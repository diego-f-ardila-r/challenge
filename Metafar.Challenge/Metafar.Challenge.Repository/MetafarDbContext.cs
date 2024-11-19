using Metafar.Challenge.Entity;
using Microsoft.EntityFrameworkCore;

namespace Metafar.Challenge.Repository;

public class MetafarDbContext : DbContext
{
    public MetafarDbContext(){}
    public MetafarDbContext(DbContextOptions<MetafarDbContext> options)
        : base(options)
    {
    } 
    public DbSet<CardEntity> Cards { get; set; }
    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<OperationEntity> Operations { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(Environment.GetEnvironmentVariable("DB"))
            .UseSnakeCaseNamingConvention();
    }
}