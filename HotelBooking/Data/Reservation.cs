using System;

namespace HotelBooking.Data
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int HowManyDays { get; set; }
        public int NumberOfPeople { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public Guid ReservationNumber { get; set; }
        public string Email { get; set; }
        public bool IsPaid { get; set; }
        public bool IsPaymentSuccessful { get; set; }
        public bool IsBookingSuccessful { get; set; }
        public bool IsEmailSendSuccessful { get; set; }
        public bool IsReservationSuccessful { get; set; }

    }
}
