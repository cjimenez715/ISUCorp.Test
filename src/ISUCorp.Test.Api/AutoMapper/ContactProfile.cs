using AutoMapper;
using ISUCorp.Test.Api.Domain.AggregatesModel.ContactTypeAggregate;
using ISUCorp.Test.Api.Domain.ContactAggregate;
using ISUCorp.Test.Api.Dtos.Contact;
using ISUCorp.Test.Api.Dtos.ContactType;

namespace ISUCorp.Test.Api.AutoMapper
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactType, ContactTypeSearchDto>();
            CreateMap<ContactTypeSearchDto, ContactType>();

            CreateMap<Contact, ContactUpdateDto>();
            CreateMap<ContactSaveDto, Contact>();
            CreateMap<ContactUpdateDto, Contact>()
                .ForMember(p => p.ContactTypeId, o => o.MapFrom(s => s.ContactType.ContactTypeId));        }
    }
}
