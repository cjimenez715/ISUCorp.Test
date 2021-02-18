using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Domain.ContactAggregate
{
    public class ContactService : IContactService
    {
        private readonly IRepository _repository;

        public ContactService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> SaveContact(Contact contact)
        {
            await _repository.SaveContact(contact);

            return await _repository.Commit();
        }

        public async Task<bool> UpdateContact(Contact contact, Contact candidate)
        {
            contact.Update(candidate.Name, candidate.BirthDate, candidate.PhoneNumber, candidate.ContactTypeId);

            await _repository.UpdateContact(contact);

            return await _repository.Commit();
        }

        public async Task RemoveContact(Contact contact)
        {
            await _repository.RemoveContact(contact);
            await _repository.Commit();
        }
    }
}
