using ISUCorp.Test.Api.Data;
using ISUCorp.Test.Api.Data.Mapping.Helpers;
using ISUCorp.Test.Api.Domain.AggregatesModel.ContactTypeModel;
using ISUCorp.Test.Api.Domain.ContactModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Domain
{
    //Repository created for DataAccess Only
    public class Repository : IRepository
    {
        private readonly DataContext _db;

        public Repository(DataContext db)
        {
            _db = db;
        }

        public async Task<List<ContactType>> GetContactTypeByFilter(string filter)
        {
            return await _db.ContactType.Where(p => p.Name.ToLower().Contains(filter.ToLower())).ToListAsync();
        }

        public async Task<List<ContactType>> GetAllContactType()
        {
            return await _db.ContactType.ToListAsync();
        }

        #region Contact
        public async Task<Contact> GetContactById(int contactId)
        {
            return await _db.Contact.Include(p => p.ContactType)
                            .FirstOrDefaultAsync(p => p.ContactId.Equals(contactId));
        }

        public async Task<Contact> GetContactByName(string name)
        {
            return await _db.Contact.AsNoTracking().FirstOrDefaultAsync(p => p.Name.ToLower().Equals(name.ToLower()));
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
            return await _db.Contact.AsNoTracking().Include(p => p.ContactType)
                        .Where(p => p.Name.ToLower()
                        .Contains(filter.ToLower())).ToListAsync();
        }

        public async Task<PagerBase<ContactResult>> GetContactPager(int pageNumber)
        {
            var pageNumberParam = new SqlParameter("@pageNumber", pageNumber);
            var items = await _db.ContactResult
                   .FromSqlInterpolated($"exec sp_get_contacts_pager {pageNumberParam}")
                   .ToListAsync();
            var count = _db.Contact.Count();

            return new PagerBase<ContactResult>() { Items = items, Count = count };
        }
        #endregion

        #region Reservation

        public async Task<Reservation> GetReservationById(int reservationId)
        {
            return await _db.Reservation
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ReservationId.Equals(reservationId));
        }

        public async Task SaveReservation(Reservation reservation)
        {
            await _db.Reservation.AddRangeAsync(reservation);
        }

        public async Task<PagerBase<ReservationResult>> GetReservationPager(int sortOption, int pageNumber)
        {
            var sortOptionParam = new SqlParameter("@sortOption", sortOption);
            var pageNumberParam = new SqlParameter("@pageNumber", pageNumber);
            var items = await _db.ReservationResult
                   .FromSqlInterpolated($"exec sp_get_reservations_pager {sortOptionParam}, {pageNumberParam}")
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
