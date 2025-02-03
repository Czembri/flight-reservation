using ReservationAPI.Enums;

namespace ReservationAPI.Model
{
    public class Reservation()
    {
        public Guid Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public TicketClass Class { get; set; }
    }

}
