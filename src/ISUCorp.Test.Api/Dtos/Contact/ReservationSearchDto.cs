using System;

namespace ISUCorp.Test.Api.Dtos.Contact
{
    //Reservation Search DTO for UI Access
    public class ReservationSearchDto
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Content { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public DateTime ContactBirthDate { get; set; }
    }
}
