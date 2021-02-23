using Microsoft.EntityFrameworkCore;
using System;

namespace ISUCorp.Test.Api.Data.Mapping.Helpers
{
    //Class no mapped created for retrieving Contact data
    [Keyless]
    public class ContactResult
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string ContactTypeName { get; set; }
    }
}
