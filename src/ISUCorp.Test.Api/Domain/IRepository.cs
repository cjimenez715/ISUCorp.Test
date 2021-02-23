using ISUCorp.Test.Api.Data.Mapping.Helpers;
using ISUCorp.Test.Api.Domain.AggregatesModel.ContactTypeModel;
using ISUCorp.Test.Api.Domain.ContactModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Domain
{
    public interface IRepository
    {
        #region ContactType
        Task<List<ContactType>> GetContactTypeByFilter(string filter);
        Task<List<ContactType>> GetAllContactType();
        #endregion

        #region Contact
        Task SaveContact(Contact contact);
        Task UpdateContact(Contact contact);
        Task<Contact> GetContactById(int contactId);
        Task<Contact> GetContactByName(string name);
        Task RemoveContact(Contact contact);
        Task<List<Contact>> GetContactByFilter(string filter);
        Task<PagerBase<ContactResult>> GetContactPager(int pageNumber);
        #endregion

        #region Reservation
        Task<Reservation> GetReservationById(int reservationId);
        Task SaveReservation(Reservation reservation);
        Task<PagerBase<ReservationResult>> GetReservationPager(int sortOption, int pageNumber);
        #endregion

        Task<bool> Commit();
    }
}
