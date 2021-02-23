using ISUCorp.Test.Api.Domain.AggregatesModel.ContactTypeModel;
using System;
using System.Collections.Generic;

namespace ISUCorp.Test.Api.Domain.ContactModel
{
    //No Anemic class created for Contact
    public class Contact
    {
        public int ContactId { get; set; }
        public string Name { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string PhoneNumber { get; private set; }

        public int ContactTypeId { get; private set; }
        public ContactType ContactType { get; private set; }

        //Injecting Dependencies
        private readonly List<Reservation> _reservations;
        public IReadOnlyCollection<Reservation> Reservations => _reservations;

        protected Contact()
        {
            _reservations = new List<Reservation>();
        }

        public Contact(string name, DateTime birthDate, string phoneNumber, int contactTypeId)
        {
            Name = name;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            ContactTypeId = contactTypeId;

            _reservations = new List<Reservation>();
        }

        //Method created for Reservation Insert logic
        public Contact(int contactId, string name, DateTime birthDate, string phoneNumber, int contactTypeId)
        {
            ContactId = contactId;
            Name = name;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            ContactTypeId = contactTypeId;

            _reservations = new List<Reservation>();
        }

        //Method created for Updating Contact Data
        public void Update(string name, DateTime birthDate, string phoneNumber, int contactTypeId)
        {
            Name = name;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            ContactTypeId = contactTypeId;
        }


        public bool IsUnassigned()
        {
            return ContactId == 0;
        }
    }
}
