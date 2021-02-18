﻿// <auto-generated />
using System;
using ISUCorp.Test.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ISUCorp.Test.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ISUCorp.Test.Api.Data.Mapping.Helpers.ReservationResult", b =>
                {
                    b.Property<DateTime>("ContactBirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.ToTable("ReservationResult");
                });

            modelBuilder.Entity("ISUCorp.Test.Api.Domain.AggregatesModel.ContactTypeAggregate.ContactType", b =>
                {
                    b.Property<int>("ContactTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactTypeId");

                    b.ToTable("ContactType");

                    b.HasData(
                        new
                        {
                            ContactTypeId = 1,
                            Name = "Contact Type 1"
                        },
                        new
                        {
                            ContactTypeId = 2,
                            Name = "Contact Type 2"
                        },
                        new
                        {
                            ContactTypeId = 3,
                            Name = "Contact Type 3"
                        });
                });

            modelBuilder.Entity("ISUCorp.Test.Api.Domain.ContactAggregate.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ContactTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactId");

                    b.HasIndex("ContactTypeId");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("ISUCorp.Test.Api.Domain.ContactAggregate.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ReservationId");

                    b.HasIndex("ContactId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("ISUCorp.Test.Api.Domain.ContactAggregate.Contact", b =>
                {
                    b.HasOne("ISUCorp.Test.Api.Domain.AggregatesModel.ContactTypeAggregate.ContactType", "ContactType")
                        .WithMany()
                        .HasForeignKey("ContactTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContactType");
                });

            modelBuilder.Entity("ISUCorp.Test.Api.Domain.ContactAggregate.Reservation", b =>
                {
                    b.HasOne("ISUCorp.Test.Api.Domain.ContactAggregate.Contact", "Contact")
                        .WithMany("Reservations")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("ISUCorp.Test.Api.Domain.ContactAggregate.Contact", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
