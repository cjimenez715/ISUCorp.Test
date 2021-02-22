

using System;

namespace ISUCorp.Test.Api.Dtos.Contact
{
    public class ReservationSaveDto
    {
        public ContactUpdateDto Contact { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Content { get; set; }
    }
}
