using System;

namespace ISUCorp.Test.Api.Dtos.Contact
{
    //Contact Save DTO for UI Acccess
    public class ContactSaveDto
    {
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public int ContactTypeId { get; set; }
    }
}
