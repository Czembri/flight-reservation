using ReservationAPI.Enums;

namespace ReservationAPI.Models
{
    public class ReservationRequest
    {
        public Guid? Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public TicketClass Class { get; set; }

    }
}
