﻿using ISUCorp.Test.Api.Domain.ContactAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISUCorp.Test.Api.Data.Mapping
{
    public class ContactMapping : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(p => p.ContactId);
            builder.Property(p => p.ContactId).HasColumnName("ContactId").UseIdentityColumn();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
            builder.Property(p => p.BirthDate).HasColumnName("BirthDate").IsRequired();
            builder.Property(p => p.PhoneNumber).HasColumnName("PhoneNumber");
            builder.Property(p => p.ContactTypeId).HasColumnName("ContactTypeId").IsRequired();

            builder.ToTable("Contact");

        }
    }
}
