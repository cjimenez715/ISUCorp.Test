using ISUCorp.Test.Api.Data.Mapping.Helpers;
using ISUCorp.Test.Api.Domain.AggregatesModel.ContactTypeAggregate;
using ISUCorp.Test.Api.Domain.ContactAggregate;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISUCorp.Test.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactType>().HasData(
                new ContactType(1, "Contact Type 1"),
                new ContactType(2, "Contact Type 2"),
                new ContactType(3, "Contact Type 3")
                );
        }

        public DbSet<ContactType> ContactType { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Reservation> Reservation { get; set; }

        [NotMapped]
        public DbSet<ReservationResult> ReservationResult { get; set; }
     }
}
