using domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.EntityTypeConfiguration;

public class CustomersConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(p => p.Name);
        builder.HasIndex(p => new 
        { 
            p.Phone, 
            p.Email
        });

        builder.Property(p => p.CompanyName)
               .HasMaxLength(int.MaxValue);
        builder.Property(p=> p.Phone).HasMaxLength(50);
    }
}
