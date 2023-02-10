using domain;
using Microsoft.EntityFrameworkCore;

namespace application.Interface;

public interface IAppDbContext
{
    public DbSet<Customer> Customers { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken token);
}