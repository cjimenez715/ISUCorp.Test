using ISUCorp.Test.Api.Data;
using ISUCorp.Test.Api.Data.Mapping.Helpers;
using ISUCorp.Test.Api.Domain.AggregatesModel.ContactTypeAggregate;
using ISUCorp.Test.Api.Domain.ContactAggregate;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Domain
{
    public class Repository : IRepository
    {
        private readonly DataContext _db;

        public Repository(DataContext db)
        {
            _db = db;
        }

        public async Task<List<ContactType>> GetContactTypeByFilter(string filter)
        {
            var result = _db.ContactType.Where(p => p.Name.ToLower().Contains(filter.ToLower()));
            return await Task.Run(() => new List<ContactType>(result));
        }

        #region Contact
        public async Task<Contact> GetContactById(int contactId)
        {
            var result = _db.Contact.Include(p => p.ContactType).FirstOrDefault(p => p.ContactId.Equals(contactId));
            return await Task.Run(() => result);
        }

        public async Task SaveContact(Contact contact)
        {
            await _db.Contact.AddRangeAsync(contact);
        }

        public async Task UpdateContact(Contact contact)
        {
            _db.Contact.Update(contact);
            await Task.CompletedTask;
        }

        public async Task RemoveContact(Contact contact)
        {
            _db.Contact.Remove(contact);
            await Task.CompletedTask; 
        }

        public async Task<List<Contact>> GetContactByFilter(string filter)
        {
            var result = _db.Contact.AsNoTracking().Include(p => p.ContactType).Where(p => p.Name.ToLower().Contains(filter.ToLower()));
            return await Task.Run(() => new List<Contact>(result));
        }
        #endregion

        #region Reservation

        public async Task<Reservation> GetReservationById(int reservationId)
        {
            return await _db.Reservation
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ReservationId.Equals(reservationId));
        }

        public async Task<PagerBase<ReservationResult>> GetReservationList(int sortOption, int pageNumber)
        {
            var sortOptionParam = new SqlParameter("@sortOption", sortOption);
            var pageNumberParam = new SqlParameter("@pageNumber", pageNumber);
            var items = await _db.ReservationResult
                   .FromSqlInterpolated($"exec sp_get_reservations_by_filter {sortOptionParam}, {pageNumberParam}")
                   .ToListAsync();
            var count = _db.Reservation.Count();

            return new PagerBase<ReservationResult>() { Items = items, Count = count };
        }
        #endregion

        public async Task<bool> Commit()
        {
            return await _db.SaveChangesAsync() != 0;
        }
    }
}
