using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Dotnet.Portal.Domain.Models;

namespace Dotnet.Portal.Data.Mappings
{
    public class DonationMapping : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder
                .HasKey(d => d.Id);

            builder
                .Property(d => d.Date)
                .IsRequired()
                .HasColumnType("date");

            builder
                .Property(d => d.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder
                .Property(d => d.Type)
                .IsRequired();

            builder
                .ToTable("Donations");
        }
    }
}
