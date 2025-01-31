namespace ReservationAPI.Models
{
    public class ReservationRequest
    {
        public Guid? Id { get; set; }
        public string PassengerName { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string Class { get; set; }
    }
}
