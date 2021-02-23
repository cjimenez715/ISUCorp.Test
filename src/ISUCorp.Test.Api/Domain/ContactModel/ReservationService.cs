using FluentValidation.Results;
using ISUCorp.Test.Api.Domain.Notifier;
using ISUCorp.Test.Api.Domain.Resources.DomainValidation;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Domain.ContactModel
{
    //Class Created for Reservation Business Rules
    public class ReservationService : NotifierService, IReservationService
    {
        //Injecting dependencies
        private readonly IRepository _repository;
        private readonly IStringLocalizer<DomainValidationResource> _localizer;

        public ReservationService(IRepository repository, IStringLocalizer<DomainValidationResource> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        //Business rules and validations for Reservation Save
        public async Task<bool> SaveReservation(Reservation reservation)
        {
            var contactValidation = await _repository.GetContactByName(reservation.Contact.Name);
            if (reservation.Contact.IsUnassigned())
            {
                if (contactValidation != null)
                {
                    AddValidationFailure(_localizer["ContactNameAlreadyExists"]);
                    return false;
                }

                await _repository.SaveReservation(reservation);
            } 
            else
            {
                if (contactValidation != null && contactValidation.ContactId != reservation.Contact.ContactId)
                {
                    AddValidationFailure(_localizer["ContactNameAlreadyExists"]);
                    return false;
                }

                await _repository.UpdateContact(reservation.Contact);
                await _repository.SaveReservation(reservation);
            }

            return await _repository.Commit();
        }

        //Getting Validation result
        public override ValidationResult ValidationResult()
        {
            return GetValidationResult();
        }
    }
}
