using ISUCorp.Test.Api.Data.Mapping.Helpers;
using ISUCorp.Test.Api.Domain.AggregatesModel.ContactTypeAggregate;
using ISUCorp.Test.Api.Domain.ContactAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Domain
{
    public interface IRepository
    {
        #region ContactType
        Task<List<ContactType>> GetContactTypeByFilter(string filter);
        #endregion

        #region Contact
        Task SaveContact(Contact contact);
        Task UpdateContact(Contact contact);
        Task<Contact> GetContactById(int contactId);
        Task RemoveContact(Contact contact);
        Task<List<Contact>> GetContactByFilter(string filter);
        #endregion

        #region Reservation
        Task<Reservation> GetReservationById(int reservationId);
        Task<PagerBase<ReservationResult>> GetReservationList(int sortOption, int pageNumber);
        #endregion

        Task<bool> Commit();
    }
}
