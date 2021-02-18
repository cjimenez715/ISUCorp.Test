using ISUCorp.Test.Api.Domain.ContactAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISUCorp.Test.Api.Data.Mapping
{
    public class ReservationMapping : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(p => p.ReservationId);
            builder.Property(p => p.ReservationId).HasColumnName("ReservationId").UseIdentityColumn();
            builder.Property(p => p.Content).HasColumnName("Content").IsRequired();
            builder.Property(p => p.ReservationDate).HasColumnName("ReservationDate").IsRequired();
            builder.Property(p => p.ContactId).HasColumnName("ContactId");

            builder.ToTable("Reservation");
        }
    }
}
