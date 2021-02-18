using Microsoft.EntityFrameworkCore;
using System;

namespace ISUCorp.Test.Api.Data.Mapping.Helpers
{
    [Keyless]
    public class ReservationResult
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Content { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public DateTime ContactBirthDate { get; set; }
    }
}
