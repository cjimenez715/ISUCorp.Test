﻿using System;

namespace ISUCorp.Test.Api.Dtos.Contact
{
    //Contact Update DTO for UI Acccess
    public class ContactUpdateDto
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public int ContactTypeId { get; set; }
    }
}
