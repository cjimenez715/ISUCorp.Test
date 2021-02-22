using AutoMapper;
using ISUCorp.Test.Api.Domain.ContactModel;
using ISUCorp.Test.Api.Dtos.Contact;
using System;

namespace ISUCorp.Test.Api.AutoMapper
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<ReservationSaveDto, Reservation>()
                .ForMember(p => p.ReservationDate, o => o.MapFrom(s => DateTime.Now));
        }
    }
}
