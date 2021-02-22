
namespace ISUCorp.Test.Api.Domain.AggregatesModel.ContactTypeModel
{
    public class ContactType
    {
        public int ContactTypeId { get; private set; }
        public string Name { get; private set; }

        protected ContactType()
        {

        }

        public ContactType(int contactTypeId, string name)
        {
            ContactTypeId = contactTypeId;
            Name = name;
        }

    }
}
