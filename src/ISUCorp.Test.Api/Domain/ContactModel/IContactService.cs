using ISUCorp.Test.Api.Domain.Notifier;
using System.Threading.Tasks;

namespace ISUCorp.Test.Api.Domain.ContactModel
{
    public interface IContactService : INotifierService
    {
        Task<bool> SaveContact(Contact contact);
        Task<bool> UpdateContact(Contact contact, Contact candidate);
        Task RemoveContact(Contact contact);
    }
}
