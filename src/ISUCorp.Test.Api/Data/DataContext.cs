using ISUCorp.Test.Api.Data.Mapping.Helpers;
using ISUCorp.Test.Api.Domain.AggregatesModel.ContactTypeModel;
using ISUCorp.Test.Api.Domain.ContactModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISUCorp.Test.Api.Data
{
    //Creating DataContexts
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        //Generating init data for ContactType table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactType>().HasData(
                new ContactType(1, "Contact Type 1"),
                new ContactType(2, "Contact Type 2"),
                new ContactType(3, "Contact Type 3")
                );
        }

        //Adding DataSets for DataAccess
        public DbSet<ContactType> ContactType { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Reservation> Reservation { get; set; }

        //Seeting NotMapped datasets for executing SP's
        [NotMapped]
        public DbSet<ReservationResult> ReservationResult { get; set; }
        [NotMapped]
        public DbSet<ContactResult> ContactResult { get; set; }
    }
}
