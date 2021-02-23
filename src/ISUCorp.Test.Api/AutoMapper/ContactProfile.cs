using AutoMapper;
using ISUCorp.Test.Api.Domain.AggregatesModel.ContactTypeModel;
using ISUCorp.Test.Api.Domain.ContactModel;
using ISUCorp.Test.Api.Dtos.Contact;
using ISUCorp.Test.Api.Dtos.ContactType;
using ISUCorp.Test.Api.Extensions;

namespace ISUCorp.Test.Api.AutoMapper
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactType, ContactTypeSearchDto>();
            CreateMap<ContactTypeSearchDto, ContactType>();

            CreateMap<Contact, ContactUpdateDto>();

            CreateMap<ContactUpdateDto, Contact>()
                .ForMember(c => c.Name, o => o.MapFrom(s => s.Name.ReplaceNullByEmpty().DeleteWhiteSpaces()))
                .ForMember(c => c.PhoneNumber, o => o.MapFrom(s => s.PhoneNumber.ReplaceNullByEmpty().DeleteWhiteSpaces()));

        }
    }
}
