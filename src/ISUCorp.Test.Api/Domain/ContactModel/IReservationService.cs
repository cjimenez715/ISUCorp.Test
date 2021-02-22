using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Domain.ContactModel
{
    public interface IReservationService
    {
        Task<bool> SaveReservation(Reservation reservation);
    }
}
