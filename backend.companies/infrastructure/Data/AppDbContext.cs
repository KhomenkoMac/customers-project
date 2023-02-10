using application.Interface;
using domain;
using infrastructure.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data;


public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomersConfiguration());
    }

}