using System;

namespace ISUCorp.Test.Api.Domain.ContactModel
{
    //No Anemic class created for Reservation
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

        //Method created for Setting Contact to Reservation
        public void SetContact(Contact contact)
        {
            Contact = contact;
        }
    }
}
