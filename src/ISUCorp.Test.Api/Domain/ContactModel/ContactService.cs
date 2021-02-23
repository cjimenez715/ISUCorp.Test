using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Localization;
using ISUCorp.Test.Api.Domain.Resources.DomainValidation;
using ISUCorp.Test.Api.Domain.Notifier;
using FluentValidation.Results;

namespace ISUCorp.Test.Api.Domain.ContactModel
{
    //Class Created for Contact Business Rules
    public class ContactService : NotifierService, IContactService
    {
        //Injecting Dependencies
        private readonly IRepository _repository;
        private readonly IStringLocalizer<DomainValidationResource> _localizer;

        public ContactService(IRepository repository, IStringLocalizer<DomainValidationResource> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        //Business rules and validations for Contact Save
        public async Task<bool> SaveContact(Contact contact)
        {
            var contactValidation = await _repository.GetContactByName(contact.Name);

            if(contactValidation != null)
            {
                AddValidationFailure(_localizer["ContactNameAlreadyExists"]);
                return false;
            }

            await _repository.SaveContact(contact);

            return await _repository.Commit();
        }

        //Business rules and validations for Contact Update
        public async Task<bool> UpdateContact(Contact contact, Contact candidate)
        {
            var contactValidation = await _repository.GetContactByName(candidate.Name);

            if (contactValidation != null && contactValidation.ContactId != contact.ContactId)
            {
                AddValidationFailure(_localizer["ContactNameAlreadyExists"]);
                return false;
            }

            contact.Update(candidate.Name, candidate.BirthDate, candidate.PhoneNumber, candidate.ContactTypeId);

            await _repository.UpdateContact(contact);

            return await _repository.Commit();
        }

        //Business rules and validations for Contact Delete
        public async Task RemoveContact(Contact contact)
        {
            await _repository.RemoveContact(contact);
            await _repository.Commit();
        }

        public override ValidationResult ValidationResult()
        {
            return GetValidationResult();
        }
    }
}
