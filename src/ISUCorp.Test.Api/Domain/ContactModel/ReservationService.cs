using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Domain.ContactModel
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository _repository;

        public ReservationService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> SaveReservation(Reservation reservation)
        {
            if(reservation.Contact.IsUnassigned())
            {
                await _repository.SaveReservation(reservation);
            } 
            else
            {
                await _repository.UpdateContact(reservation.Contact);
                await _repository.SaveReservation(reservation);
            }

            return await _repository.Commit();
        }
    }
}
