namespace TicketVerkoop.ViewModels
{
    public class HotelVM
    {
        public List<Hotel> Hotels { get; set; }
    }

    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
