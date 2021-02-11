namespace HotelBooking.Data
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal PriceForOnePerson { get; set; }
        public bool IsPaymentNecessary { get; set; }
    }
}
