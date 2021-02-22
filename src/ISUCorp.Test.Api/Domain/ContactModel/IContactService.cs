using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Domain.ContactModel
{
    public interface IContactService
    {
        Task<bool> SaveContact(Contact contact);
        Task<bool> UpdateContact(Contact contact, Contact candidate);
        Task RemoveContact(Contact contact);
    }
}
