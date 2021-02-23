using System;

namespace ISUCorp.Test.Api.Domain.ContactModel
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; private set; }
        public string Content { get; set; }
        public int ContactId { get; private set; }
        public Contact Contact { get; private set; }

        protected Reservation()
        {

        }

        public Reservation(DateTime reservationDate, string content, int contactId)
        {
            ReservationDate = reservationDate;
            Content = content;
            ContactId = contactId;
        }

        public void Update(DateTime reservationDate, string content, int contactId)
        {
            ReservationDate = reservationDate;
            Content = content;
            ContactId = contactId;
        }

        public void SetContact(Contact contact)
        {
            Contact = contact;
        }
    }
}
