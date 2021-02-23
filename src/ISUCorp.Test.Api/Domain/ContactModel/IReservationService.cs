using ISUCorp.Test.Api.Domain.Notifier;
using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Domain.ContactModel
{
    //Inteface for settings Reservation Service Contract
    public interface IReservationService: INotifierService
    {
        Task<bool> SaveReservation(Reservation reservation);
    }
}
