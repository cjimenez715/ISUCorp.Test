using ISUCorp.Test.Api.Domain.AggregatesModel.ContactTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISUCorp.Test.Api.Data.Mapping
{
    public class ContactTypeMapping : IEntityTypeConfiguration<ContactType>
    {
        public void Configure(EntityTypeBuilder<ContactType> builder)
        {
            builder.HasKey(p => p.ContactTypeId);
            builder.Property(p => p.ContactTypeId).HasColumnName("ContactTypeId").UseIdentityColumn();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();

            builder.ToTable("ContactType");
        }
    }
}
