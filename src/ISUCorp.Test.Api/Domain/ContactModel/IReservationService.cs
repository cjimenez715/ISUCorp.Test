using ISUCorp.Test.Api.Domain.Notifier;
using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Domain.ContactModel
{
    public interface IReservationService: INotifierService
    {
        Task<bool> SaveReservation(Reservation reservation);
    }
}
