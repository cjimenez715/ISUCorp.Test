using ISUCorp.Test.Api.Domain.AggregatesModel.ContactTypeModel;
using System;
using System.Collections.Generic;

namespace ISUCorp.Test.Api.Domain.ContactModel
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string Name { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string PhoneNumber { get; private set; }

        public int ContactTypeId { get; private set; }
        public ContactType ContactType { get; private set; }

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
